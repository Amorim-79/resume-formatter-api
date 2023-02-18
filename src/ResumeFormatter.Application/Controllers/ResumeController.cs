using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeFormatter.Domain.Interfaces.Service;

namespace ResumeFormatter.Application.Controllers;

[Authorize("Bearer")]
[ApiController]
[Route("api/[controller]")]
public class ResumeController : ControllerBase
{
    private readonly ILogger<ResumeController> _logger;
    public ResumeController(ILogger<ResumeController> logger)
    {
        this._logger = logger;
    }

    [HttpPost("Format")]
    public IActionResult Format([FromServices] IResumeService resumeService, IFormFile file, IFormFile template)
    {
        try
        {
            return File(resumeService.Format(template, file), file.ContentType, file.FileName);
        }
        catch (Exception error)
        {
            return BadRequest(error);
        }
    }
}
