using Microsoft.AspNetCore.Mvc;
using Auth.Model;
using System;
using Auth.Model.Validators;
using System.Collections.Generic;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Auth.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
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
                if(e.Message.Equals("Error with database connection."))
                {
                    // return BadRequest(new { StatusCode = 500, Message = e.Message });
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                return BadRequest(new { StatusCode = 403, Message = e.Message });
                

            }      
        }

        [HttpPost("register")]
        public IActionResult Register(RegistrationRequest model)
        {
            UserValidator validator = new UserValidator();
            List<string> ValidationMessages = new List<string>();
            var validationResult = validator.Validate(model);
            var response = new ResponseModel();
            if (!validationResult.IsValid)
            {
                response.IsValid = false;
                foreach (ValidationFailure failure in validationResult.Errors)
                {
                    ValidationMessages.Add(failure.ErrorMessage);
                }
                response.ValidationMessages = ValidationMessages;
                return BadRequest(new { StatusCode = 400, Message = response.ValidationMessages });
            }
            else
            {
                try
                {
                    string token = _userService.Register(model);
                    return Ok(new { StatusCode = 200, Token = token });
                }
                catch (Exception e)
                {
                    if (e.Message.Equals("Error with database connection."))
                    {
                        // return BadRequest(new { StatusCode = 500, Message = e.Message });
                        return StatusCode(StatusCodes.Status500InternalServerError);
                    }
                    return BadRequest(new { StatusCode = 400, Message = e.Message });
                  

                }

            }         
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_userService.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
