using Microsoft.AspNetCore.Mvc;
using ResumeFormatter.Domain.Entities;
using ResumeFormatter.Domain.Interfaces.Service;

namespace ResumeFormatter.Application.Controllers
{
    [ApiController]
    [Route("[keywords]")]
    public class KeywordsController : ControllerBase
    {
        private readonly ILogger<KeywordsController> _logger;
        public KeywordsController(ILogger<KeywordsController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "create")]
        public IActionResult Create([FromServices] IBaseService<Keywords> service, Keywords Keywords)
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

        [HttpPost(Name = "list")]
        public IActionResult List([FromServices] IBaseService<Keywords> service)
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

        [HttpPost(Name = "update")]
        public IActionResult Update([FromServices] IBaseService<Keywords> service, Keywords Keywords)
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

        [HttpPost(Name = "delete")]
        public IActionResult Delete([FromServices] IBaseService<Keywords> service, int id)
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
