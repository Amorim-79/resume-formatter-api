using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using ResumeFormatter.Domain.Entities;
using ResumeFormatter.Domain.Interfaces.Service;
using System.Linq;
using System.Reflection.Metadata;
using ResumeFormatter.Service.Helpers;
using ResumeFormatter.Service.Enums.Xml;
using DocumentFormat.OpenXml;

namespace ResumeFormatter.Service.Services
{
    public class ResumeService : IResumeService
    {
        private readonly XmlUtils xmlUtils = new();
        private readonly List<Keyword> keyWords = new()
        {
            new Keyword() { Id = 1, UserId = 1, Word = "Objetivo", CreatedAt = DateTime.Now },
            new Keyword() { Id = 2, UserId = 1, Word = "Habilidades e competência", CreatedAt = DateTime.Now },
            new Keyword() { Id = 3, UserId = 1, Word = "Escolaridade", CreatedAt = DateTime.Now },
            new Keyword() { Id = 4, UserId = 1, Word = "Experiência profissional", CreatedAt = DateTime.Now },
            new Keyword() { Id = 5, UserId = 1, Word = "Conhecimentos", CreatedAt = DateTime.Now },
            new Keyword() { Id = 6, UserId = 1, Word = "Idiomas", CreatedAt = DateTime.Now }
        };

        public byte[] Format(IFormFile template, IFormFile file)
        {
            var templateStream = template.OpenReadStream();
            var fileStream = file.OpenReadStream();

            var fileData = GetFileData(fileStream);

            return this.WriteDocumentFile(templateStream, fileData);
        }

        private Dictionary<string, dynamic> GetFileData(Stream stream)
        {
            var resumeData = new Dictionary<string, dynamic>(StringComparer.OrdinalIgnoreCase);

            MemoryStream documentStream = new();
            stream.CopyTo(documentStream);

            using (WordprocessingDocument template = WordprocessingDocument.Open(documentStream, true))
            {
                template.ChangeDocumentType(DocumentFormat.OpenXml.WordprocessingDocumentType.Document);
                MainDocumentPart mainPart = template.MainDocumentPart;

                var mainRun = mainPart.Document.Body?.Descendants<Paragraph>();

                resumeData.Add("nome completo", mainRun.ElementAt(0).InnerText);

                foreach (var run in mainRun.Select((value, index) => new { value, index }))
                {
                    string runTextFormatted = run.value.InnerText.Replace(":", "");

                    if (this.keyWords.Any(keyWord => keyWord.Word == runTextFormatted))
                    {
                        string nextLineText = mainRun.ElementAt(run.index + 1).InnerText;
                        if (!string.IsNullOrEmpty(nextLineText) && nextLineText != ":")
                        {
                            string keywordFound = this.keyWords.Where(keyWord => keyWord.Word == runTextFormatted).First().Word;
                            resumeData.Add(keywordFound, this.GetCompletedTextParagraph(mainRun, (run.index + 1)));
                            continue;
                        }
                    }
                }

                mainPart.Document.Save();
            }

            documentStream.ToArray();

            return resumeData;
        }

        private byte[] WriteDocumentFile(Stream stream, Dictionary<string, dynamic> resumeData)
        {
            MemoryStream documentStream = new();
            stream.CopyTo(documentStream);

            using (WordprocessingDocument template = WordprocessingDocument.Open(documentStream, true))
            {
                template.ChangeDocumentType(DocumentFormat.OpenXml.WordprocessingDocumentType.Document);
                MainDocumentPart mainPart = template.MainDocumentPart;

                IEnumerable<Paragraph> paragraphs = mainPart.Document.Body?.Descendants<Paragraph>();

                foreach (var paragraph in paragraphs)
                {
                    string paragraphTextFormatted = paragraph.InnerText
                        .Replace("{", "")
                        .Replace("}", "")
                        .ToLower()
                        .Trim();

                    if (resumeData.TryGetValue(paragraphTextFormatted, out dynamic fieldValue))
                    {
                        bool isNameField = resumeData.First().Key == paragraphTextFormatted;

                        ParagraphProperties paragraphProperties = new();
                        Justification justification = new() { Val = this.xmlUtils.DetectParagraphJustification(paragraph) };

                        Run defaultParagraphRun = new();
                        RunProperties runProperties = defaultParagraphRun.AppendChild(new RunProperties());
                        runProperties = this.ApplyRunStyles(runProperties, paragraph);

                        paragraph.RemoveAllChildren<Run>();
                        paragraph.Append(paragraphProperties);
                        paragraphProperties.Append(justification);

                        foreach (string lineString in fieldValue.Split(new char[] { '\r' }))
                        {
                            Run run = (Run)defaultParagraphRun.CloneNode(true);
                            run.AppendChild(new Text(lineString));

                            paragraph.AppendChild<Run>(run);
                            paragraph.AppendChild<Run>(new Run(new Break()));
                        }
                    }
                }
                mainPart.Document.Save();
            }

            return documentStream.ToArray();
        }

        private string GetCompletedTextParagraph(IEnumerable<Paragraph> documentParagraphs, int initialIndex)
        {
            string completedText = String.Empty;

            var nextLineText = documentParagraphs.ElementAt(initialIndex).InnerText;
            if (!this.IsKeyWord(nextLineText))
            {
                bool hasMoreLines = documentParagraphs.Count() - 1 != initialIndex;

                completedText += @$"{nextLineText}
{(hasMoreLines ? this.GetCompletedTextParagraph(documentParagraphs, (initialIndex + 1)) : "")}";

            }

            return completedText;
        }

        private bool IsKeyWord(string stringToCompare)
        {
            stringToCompare = stringToCompare
                .Replace(":", "")
                .Replace(";", "")
                .Replace("-", "")
                .ToLower()
                .Trim();

            return this.keyWords.Any(keyWord => keyWord.Word.Trim().ToLower() == stringToCompare);
        }

        private RunProperties ApplyRunStyles(RunProperties runProperties, Paragraph paragraph)
        {
            switch (this.xmlUtils.DetectStyles(paragraph))
            {
                case ETextStyles.AllStyles:
                    runProperties.AppendChild(new Bold());
                    runProperties.AppendChild(new Italic());
                    runProperties.AppendChild(new Underline());
                    break;
                case ETextStyles.BoldItalic:
                    runProperties.AppendChild(new Bold());
                    runProperties.AppendChild(new Italic());
                    break;
                case ETextStyles.BoldUnderLine:
                    runProperties.AppendChild(new Bold());
                    runProperties.AppendChild(new Underline());
                    break;
                case ETextStyles.ItalicUnderLine:
                    runProperties.AppendChild(new Italic());
                    runProperties.AppendChild(new Underline());
                    break;
                case ETextStyles.Italic:
                    runProperties.AppendChild(new Italic());
                    break;
                case ETextStyles.Bold:
                    runProperties.AppendChild(new Bold());
                    break;
                case ETextStyles.UnderLine:
                    runProperties.AppendChild(new Underline());
                    break;
                default:
                    break;
            }

            string? fontFamily = this.xmlUtils.DetectFontFamily(paragraph);
            string? fontSize = this.xmlUtils.DetectFontSize(paragraph);

            if (!string.IsNullOrEmpty(fontFamily)) runProperties.RunFonts = new RunFonts { Ascii = fontFamily };
            if (!string.IsNullOrEmpty(fontSize)) runProperties.FontSize = new FontSize { Val = fontSize };

            return runProperties;
        }
    }
}
