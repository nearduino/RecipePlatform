using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Auth.Model;
using System.Text.RegularExpressions;
using Auth.Model.Exceptions;
using Auth.Model.InfrastructureInterfaces;
using Auth.Model.Validators;
using FluentValidation.Results;

namespace Auth.Service
{
    public class UserService : IUserService
    {

        private string secretKey = "auhfeisoruvbe0t3ertbhe45tbe5ter5gu39485793084679084256932854902375niudgh";
        private UserValidator validator;


        private readonly IUserInfrastructureService _userInfrastructureService;

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IUserInfrastructureService userInfrastructureService)
        {
            _appSettings = appSettings.Value;
            _userInfrastructureService = userInfrastructureService;
            validator = new UserValidator();
        }

        public string Authenticate(AuthenticateRequest model)
        {
            IEnumerable<User> allUsers = _userInfrastructureService.GetAll();
            var user = allUsers.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) throw new LogInException();

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return token;
        }

        public string Register(RegistrationRequest model)
        {
            IEnumerable<User> allUsers = _userInfrastructureService.GetAll();

            /*
            foreach (var u in allUsers)
            {
                if (u.UserName.Equals(model.Username))
                {
                    throw new UsernameIsTakenException();
                }
                else if (u.Email.Equals(model.Email))
                {
                    throw new EmailIsTakenException();
                }
            }
            if (!IsValid(model.Email))
            {
                throw new InvalidEmailFormatException();
            }     */      

            
            User user = new User(model.FirstName, model.LastName, model.UserName, model.Email, model.Password, model.IsAdmin);
            _userInfrastructureService.SaveUser(user);
            //ValidationResult results = validator.Validate(user);
           

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return token;
        }

        bool IsValid(string email)
        {
            
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; 
            }
            try
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
               

        public IEnumerable<User> GetAll()
        {
            return _userInfrastructureService.GetAll().ToList();
        }

        public User GetById(int id)
        {
            return _userInfrastructureService.GetById(id);
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("isAdmin", user.IsAdmin.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}