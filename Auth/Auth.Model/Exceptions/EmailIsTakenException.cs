using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Model.Exceptions
{
    public class EmailIsTakenException : ArgumentException
    {
        public EmailIsTakenException() : base("Email is already taken!")
        {

        }
    }
}
