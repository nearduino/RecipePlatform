using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Model.Exceptions
{
    public class InvalidUserIdException : ArgumentException
    {
        public InvalidUserIdException(): base("User id must be greater than zero"){}
    }
}
