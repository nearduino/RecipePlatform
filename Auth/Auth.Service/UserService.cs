using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Auth.Model;
using System.Net.Mail;

namespace Auth.Service
{
    
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        RegistrationResponse Registration(RegistrationRequest model);
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

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            db.Users.Add(new User("test", "test", "test", "test", "test"));
            var user = db.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public RegistrationResponse Registration(RegistrationRequest model)
        {
            db.Users.Add(new User("test", "test", "test", "test", "test"));
            RegistrationResponse rr;
            foreach (var u in db.Users)
            {
                if (u.Username.Equals(model.Username))
                {

                    rr = new RegistrationResponse("Username is already taken!");
                    return rr;
                    
                }
                else if (u.Email.Equals(model.Email))
                {
                    rr = new RegistrationResponse("Email is already taken!");
                    return rr;
                }
            }
            if (!IsValid(model.Email))
            {
                rr = new RegistrationResponse("Invalid email format!");
                return rr;
            }
            User user = new User(model.FirstName, model.LastName, model.Username, model.Email, model.Password);
            db.Users.Add(user);

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new RegistrationResponse(user, token);
        }

        bool IsValid(string email)
        {
            
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
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