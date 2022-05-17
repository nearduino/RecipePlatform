using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Model
{
    public interface IUserService
    {
        public IEnumerable<User> GetAll();

        public void CreateNewUser(string firstName, string lastName, string userName, string email, string password, bool isAdmin);
        public User GetById(int id);

        string Authenticate(AuthenticateRequest model);
        string Register(RegistrationRequest model);
     
       
    }
}
