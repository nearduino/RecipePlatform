using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Auth.Model;
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
        List<User> db = new List<User>();
        

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications


        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            db.Add(new User("test", "test", "test", "test", "test"));
            var user = db.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public RegistrationResponse Registration(RegistrationRequest model)
        {
            /*foreach(var u in dataBase.UsersList)
            {
                if (u.Username.Equals(model.Username))
                {
                 
                    
                }
            } */
            User user = new User(model.FirstName, model.LastName, model.Username, model.Email, model.Password);
            db.Add(user);

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new RegistrationResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return db;
        }

        public User GetById(int id)
        {
            return db.FirstOrDefault(x => x.Id == id);
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