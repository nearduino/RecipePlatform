using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Auth.Model;
using Auth.Model.Exceptions;
using Auth.Model.InfrastructureInterfaces;
using System.Security.Cryptography;

namespace Auth.Service
{
    public class UserService : IUserService
    {

        private string secretKey = "auhfeisoruvbe0t3ertbhe45tbe5ter5gu39485793084679084256932854902375niudgh";

        private readonly IUserInfrastructureService _userInfrastructureService;

        public string salt;

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications

        public UserService(IUserInfrastructureService userInfrastructureService)
        {        
            _userInfrastructureService = userInfrastructureService;
        }

        public string Authenticate(AuthenticateRequest model)
        {
            IEnumerable<User> allUsers;
            try
            {
               allUsers = _userInfrastructureService.GetAll();
            }
            catch (Exception e)
            {
                throw new DatabaseConnectionException();
            }
            var user = allUsers.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) throw new LogInException();

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return token;
        }

        public string Register(RegistrationRequest model)
        {
            IEnumerable<User> allUsers;
            try
            {
                allUsers = _userInfrastructureService.GetAll();
            }
            catch(Exception e)
            {
                throw new DatabaseConnectionException();
            }
            
            
            // checking if username or email is already taken in database, return exception 
            foreach (var u in allUsers)
            {
                if (u.UserName.Equals(model.UserName))
                {
                    throw new UsernameIsTakenException();
                }
                else if (u.Email.Equals(model.Email))
                {
                    throw new EmailIsTakenException();
                }
            }              
          
            User user = new User(model.FirstName, model.LastName, model.UserName, model.Email, PassEncoding(model.Password), model.IsAdmin);
            _userInfrastructureService.SaveUser(user);           

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return token;
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

        public static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }
        private string PassEncoding(string password)
        {
            using (var sha = SHA256.Create())
            {
                salt = CreateSalt(16);

                var computedHash = sha.ComputeHash(Encoding.Unicode.GetBytes(salt + password));

                return Convert.ToBase64String(computedHash);
            }
        }
    }
}