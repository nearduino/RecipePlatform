using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Model.Exceptions
{
    public class InvalidRecipeInstructionsException : ArgumentException
    {
        public InvalidRecipeInstructionsException():base("Instructions must not be empty"){}
    }
}
