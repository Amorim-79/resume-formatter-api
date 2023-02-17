using Microsoft.AspNetCore.Mvc;
using ResumeFormatter.Domain.Entities;
using ResumeFormatter.Domain.Interfaces.Service;

namespace ResumeFormatter.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "Register")]
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

        [HttpPost(Name = "Login")]
        public IActionResult Login([FromServices] IBaseService<User> service, User user)
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

        [HttpPost(Name = "Delete")]
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
