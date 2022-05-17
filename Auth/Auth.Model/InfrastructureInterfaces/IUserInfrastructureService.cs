using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Model.InfrastructureInterfaces
{
    public interface IUserInfrastructureService
    {
        public IEnumerable<User> GetAll();
        public void SaveUser(User user);
        public User GetById(int id);
    }
}

