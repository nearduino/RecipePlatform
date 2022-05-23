using Auth.Infrastructure.DBO;
using Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Mappers
{
    public class Mapper
    {
        public static User Map(UserDbo dbo)
        {
            if (dbo == null) return null;                             
            return new User(dbo.FirstName,
                dbo.LastName,
                dbo.UserName,
                dbo.Email,
                dbo.Password,
                dbo.IsAdmin,
                dbo.Id,
                dbo.Salt);
        }

        public static UserDbo Map(User user)
        {
            if (user == null) return null;          

            UserDbo dbo = new UserDbo
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                IsAdmin = user.IsAdmin,         
                Salt = user.Salt
            };
            return dbo;
        }
    }
}
