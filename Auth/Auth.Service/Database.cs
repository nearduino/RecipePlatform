using Auth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
