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

namespace Auth.Service
{
    
    public interface IUserService
    {
        string Authenticate(AuthenticateRequest model);
        string Register(RegistrationRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        
    }

    public class UserService : IUserService
    {

        static Database db = new Database();

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string Authenticate(AuthenticateRequest model)
        {
            var user = db.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

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
                if (u.Username.Equals(model.Username))
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
            User user = new User(model.FirstName, model.LastName, model.Username, model.Email, model.Password);
            db.Users.Add(user);

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
            return db.Users;
        }

        public User GetById(int id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
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