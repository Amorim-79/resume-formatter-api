using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using ResumeFormatter.Domain.Interfaces.Service;

namespace ResumeFormatter.Service.Services
{
    public class ResumeService : IResumeService
    {
        private List<string> keyWords = new List<string>()
        {
            "Objetivo",
            "Habilidades e competência",
            "Escolaridade",
            "Experiência profissional",
            "Conhecimentos",
            "Idiomas",
            "Objetivo:",
            "Habilidades e competência:",
            "Escolaridade:",
            "Experiência profissional:",
            "Conhecimentos:",
            "Idiomas:"
        };

        public byte[] Format(IFormFile template, IFormFile file)
        {
            var templateStream = template.OpenReadStream();
            var fileStream = file.OpenReadStream();

            var fileData = GetFileData(fileStream);

            return this.ProcessDocumentFile(templateStream, fileData);
        }

        private Dictionary<string, dynamic> GetFileData(Stream stream)
        {
            MemoryStream documentStream = new MemoryStream();
            stream.CopyTo(documentStream);

            using (WordprocessingDocument template = WordprocessingDocument.Open(documentStream, true))
            {
                template.ChangeDocumentType(DocumentFormat.OpenXml.WordprocessingDocumentType.Document);
                MainDocumentPart mainPart = template.MainDocumentPart;

                var mainRun = mainPart.Document.Body?.Descendants<Run>();

                foreach (var run in mainRun.Select((value, index) => new { value, index }))
                {
                    foreach (RunProperties props in run.value?.Descendants<RunProperties>())
                    {
                        if (props.Descendants<Bold>().Count() > 0 && props.Descendants<Bold>()?.First() != null && this.keyWords.Contains(run.value.InnerText))
                        {
                            var aa = mainRun.ElementAt(run.index + 1).InnerText;
                        }
                    }
                }

                mainPart.Document.Save();

            }

            documentStream.ToArray();

            return new Dictionary<string, dynamic>();
        }

        private byte[] ProcessDocumentFile(Stream stream, Dictionary<string, dynamic> fileData)
        {
            MemoryStream documentStream = new MemoryStream();
            stream.CopyTo(documentStream);

            using (WordprocessingDocument template = WordprocessingDocument.Open(documentStream, true))
            {
                template.ChangeDocumentType(DocumentFormat.OpenXml.WordprocessingDocumentType.Document);
                MainDocumentPart mainPart = template.MainDocumentPart;

                mainPart.Document.Save();
            }

            return documentStream.ToArray();
        }

        private byte[] EditTextFields(Stream fileStream)
        {
            return null;
        }
    }
}
