using Microsoft.AspNetCore.Mvc;
using ResumeFormatter.Domain.Entities;
using ResumeFormatter.Domain.Interfaces.Service;

namespace ResumeFormatter.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserKeywordsController : ControllerBase
    {
        private readonly ILogger<UserKeywordsController> _logger;
        public UserKeywordsController(ILogger<UserKeywordsController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "Create")]
        public IActionResult Create([FromServices] IBaseService<UserKeywords> service, UserKeywords userKeywords)
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

        [HttpPost(Name = "List")]
        public IActionResult List([FromServices] IBaseService<UserKeywords> service)
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

        [HttpPost(Name = "Update")]
        public IActionResult Update([FromServices] IBaseService<UserKeywords> service, UserKeywords userKeywords)
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

        [HttpPost(Name = "Delete")]
        public IActionResult Delete([FromServices] IBaseService<UserKeywords> service, int id)
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
