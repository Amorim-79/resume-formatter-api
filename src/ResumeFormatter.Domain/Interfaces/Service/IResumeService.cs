using Microsoft.AspNetCore.Http;

namespace ResumeFormatter.Domain.Interfaces.Service
{
    public interface IResumeService
    {
        byte[] Format(IFormFile template, IFormFile file);
    }
}
