using Microsoft.AspNetCore.Mvc;

namespace ResumeFormatter.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class ResumeController : ControllerBase
{
    private readonly ILogger<ResumeController> _logger;
    public ResumeController(ILogger<ResumeController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "Format")]
    public IActionResult Format(IFormFile file, IFormFile template)
    {
        try
        {
            var retorno = File(new ResumeService().Format(template, file), file.ContentType, file.FileName);
            return retorno;
        }
        catch (Exception error)
        {
            return BadRequest(error);
        }
    }
}
