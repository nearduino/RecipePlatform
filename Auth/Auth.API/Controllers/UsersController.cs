using Microsoft.AspNetCore.Mvc;
using Auth.Model;
using Auth.Service;
using System;

namespace Auth.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            try
            {
                var response = _userService.Authenticate(model);
                return Ok(new { StatusCode = 200, Token = response });
            }
            catch(Exception e)
            {
                return BadRequest(new { StatusCode = 403, Message = e.Message });

            }      
        }

        [HttpPost("register")]
        public IActionResult Register(RegistrationRequest model)
        {
            try
            {
                var response = _userService.Register(model);
                return Ok(new { StatusCode = 200, Token = response });
            }
            catch (Exception e)
            {
                return BadRequest(new { StatusCode = 400, Message =  e.Message});
            }
            
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
