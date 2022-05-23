using System;
using System.Collections.Generic;


namespace Auth.Model.InfrastructureInterfaces
{
    public interface IUserInfrastructureService
    {
        public IEnumerable<User> GetAll();
        public void SaveUser(User user);
        public User GetById(Guid id);
    }
}

