using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeFormatter.Domain.Entities;
using ResumeFormatter.Domain.Interfaces.Service;

namespace ResumeFormatter.Application.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class KeywordsController : ControllerBase
    {
        private readonly ILogger<KeywordsController> _logger;
        public KeywordsController(ILogger<KeywordsController> logger)
        {
            _logger = logger;
        }

        [HttpPost("Create")]
        public IActionResult Create([FromServices] IBaseService<Keyword> service, Keyword Keyword)
        {
            try
            {
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpGet("List")]
        public IActionResult List([FromServices] IBaseService<Keyword> service)
        {
            try
            {
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpPut("Update")]
        public IActionResult Update([FromServices] IBaseService<Keyword> service, Keyword Keyword)
        {
            try
            {
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete([FromServices] IBaseService<Keyword> service, int id)
        {
            try
            {
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
