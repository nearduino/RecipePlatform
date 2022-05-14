using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Model.Exceptions
{
    public class InvalidDescriptionException : ArgumentException
    {
        public InvalidDescriptionException():base("Recipe must contain description"){}
    }
}
