using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using ResumeFormatter.Domain.Entities;
using ResumeFormatter.Domain.Interfaces.Service;
using System.Linq;
using System.Reflection.Metadata;

namespace ResumeFormatter.Service.Services
{
    public class ResumeService : IResumeService
    {
        private List<Keyword> keyWords = new List<Keyword>()
        {
            new Keyword() { Id = 1, UserId = 1, Word = "Objetivo", CreatedAt = DateTime.Now },
            new Keyword() { Id = 2, UserId = 1, Word = "Habilidades e competência", CreatedAt = DateTime.Now },
            new Keyword() { Id = 3, UserId = 1, Word = "Escolaridade", CreatedAt = DateTime.Now },
            new Keyword() { Id = 4, UserId = 1, Word = "Experiência profissional", CreatedAt = DateTime.Now },
            new Keyword() { Id = 5, UserId = 1, Word = "Conhecimentos", CreatedAt = DateTime.Now },
            new Keyword() { Id = 5, UserId = 1, Word = "Idiomas", CreatedAt = DateTime.Now }
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

            MemoryStream documentStream = new MemoryStream();
            stream.CopyTo(documentStream);

            using (WordprocessingDocument template = WordprocessingDocument.Open(documentStream, true))
            {
                template.ChangeDocumentType(DocumentFormat.OpenXml.WordprocessingDocumentType.Document);
                MainDocumentPart mainPart = template.MainDocumentPart;

                var mainRun = mainPart.Document.Body?.Descendants<Paragraph>();

                resumeData.Add("nome completo", mainRun.ElementAt(0).InnerText);

                foreach (var run in mainRun.Select((value, index) => new { value, index }))
                {
                    foreach (RunProperties props in run.value?.Descendants<RunProperties>())
                    {
                        if (props.Descendants<Bold>().Count() > 0 && props.Descendants<Bold>()?.First() != null && this.keyWords.Any(keyWord => keyWord.Word == run.value.InnerText.Replace(":", "")))
                        {
                            var nextLineText = mainRun.ElementAt(run.index + 1).InnerText;
                            if (!string.IsNullOrEmpty(nextLineText) && nextLineText != ":")
                            {
                                resumeData.Add(this.keyWords.Where(keyWord => keyWord.Word == run.value.InnerText.Replace(":", "")).First().Word,
                                    this.GetCompletedTextParagraph(mainRun, (run.index + 1)));
                                break;
                            }
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
            MemoryStream documentStream = new MemoryStream();
            stream.CopyTo(documentStream);

            using (WordprocessingDocument template = WordprocessingDocument.Open(documentStream, true))
            {
                template.ChangeDocumentType(DocumentFormat.OpenXml.WordprocessingDocumentType.Document);
                MainDocumentPart mainPart = template.MainDocumentPart;

                var paragraphs = mainPart.Document.Body?.Descendants<Paragraph>();

                foreach (var paragraph in paragraphs)
                {
                    if (resumeData.TryGetValue(paragraph.InnerText.Replace("{", "").Replace("}", "").ToLower().Trim(), out dynamic fieldValue))
                    {
                        bool isNameField = resumeData.First().Key == paragraph.InnerText.Replace("{", "").Replace("}", "").ToLower().Trim();

                        ParagraphProperties paragraphProperties = new ParagraphProperties();
                        Justification justification = new Justification() { Val = JustificationValues.Start };

                        if (isNameField)
                        {
                            justification = new Justification() { Val = JustificationValues.Center };
                        }

                        paragraph.RemoveAllChildren<Run>();
                        paragraph.Append(paragraphProperties);
                        paragraphProperties.Append(justification);

                        foreach (string lineString in fieldValue.Split(new char[] { '\r' }))
                        {
                            Run run = new Run();

                            if (isNameField)
                            {
                                Bold bold = new Bold();
                                RunProperties runProperties = run.AppendChild(new RunProperties());
                                runProperties.AppendChild(bold);
                            }

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
    }
}
