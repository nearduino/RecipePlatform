﻿using Microsoft.AspNetCore.Mvc;
using Auth.Model;
using Auth.Service;

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
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult Register(RegistrationRequest model)
        {
            var response = _userService.Registration(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            if (!response.Status)
            {
                return BadRequest(new { message = response.Message });
            }
            else
            {
                return Ok(response);
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
