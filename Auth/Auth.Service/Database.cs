using Auth.Model;
using System.Collections.Generic;

namespace Auth.Service
{
    public class Database
    {
        public List<User> Users { get; set; }

        public Database()
        {
            Users = new List<User>();
        }
    }
}
