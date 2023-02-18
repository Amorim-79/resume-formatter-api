using Microsoft.AspNetCore.Mvc;
using ResumeFormatter.Domain.Entities;
using ResumeFormatter.Domain.Interfaces.Service;

namespace ResumeFormatter.Application.Controllers
{
    [ApiController]
    [Route("[user]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            this._logger = logger;
        }

        [HttpPost(Name = "register")]
        public IActionResult Register([FromServices] IBaseService<User> service, User user)
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
        public IActionResult Update([FromServices] IBaseService<User> service, User user)
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
        public IActionResult Delete([FromServices] IBaseService<User> service, int id)
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
