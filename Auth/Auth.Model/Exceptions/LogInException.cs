using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Model.Exceptions
{
   public class LogInException : ArgumentException
    {
        public LogInException() : base("Wrong username or password!")
        {

        }
    }
}
