using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Model.Exceptions
{
   public class DatabaseConnectionException : ArgumentException
    {
        public DatabaseConnectionException() : base("Error with database connection.")
        {

        }
    }
}
