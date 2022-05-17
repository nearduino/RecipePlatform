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

namespace Auth.Service
{   
  
    public class UserService : IUserService
    {

        static Database db = new Database();

        private readonly IUserInfrastructureService _userInfrastructureService;

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IUserInfrastructureService userInfrastructureService)
        {
            _appSettings = appSettings.Value;
            _userInfrastructureService = userInfrastructureService;
        }

        public string Authenticate(AuthenticateRequest model)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) throw new LogInException();

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return token;
        }

        public string Register(RegistrationRequest model)
        {
           
            foreach (var u in db.Users)
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
            }
           
            CreateNewUser(model.FirstName, model.LastName, model.Username, model.Email, model.Password, model.IsAdmin);

            // authentication successful so generate jwt token
            User user = new User(model.FirstName, model.LastName, model.Username, model.Email, model.Password, model.IsAdmin);
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

        public void CreateNewUser(string firstName, string lastName, string userName, string email, string password, bool isAdmin)         
        {            
            User user = new User(firstName, lastName, userName, email, password, isAdmin);
            _userInfrastructureService.SaveUser(user);
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
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}