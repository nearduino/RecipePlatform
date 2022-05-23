using Auth.Infrastructure.Mappers;
using Auth.Infrastructure.Repositories;
using Auth.Model;
using Auth.Model.InfrastructureInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Auth.Service
{
    public class UserInfrastructureService : IUserInfrastructureService
    {
        private readonly IUserRepository _userRepository;

        public UserInfrastructureService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAll()
        {
            var userDbos = _userRepository.
                GetAll().               
                ToList();
            List<User> users = new List<User>();
            foreach (var user in userDbos)
            {
                users.Add(Mapper.Map(user));
            }

            return users;
        }

        public void SaveUser(User user)
        {
            _userRepository.Add(Mapper.Map(user));
        }

        public User GetById(Guid id)
        {
            return Mapper.Map(_userRepository.GetById(id));
        }

    }
}
