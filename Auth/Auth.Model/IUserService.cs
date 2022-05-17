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

        public void CreateNewUser(string firstName, string lastName, string userName, string email);
        public User GetById(int id);
    }
}
