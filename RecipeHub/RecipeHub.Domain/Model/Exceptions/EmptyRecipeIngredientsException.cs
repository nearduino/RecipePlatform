using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Model.Exceptions
{
    public class EmptyRecipeIngredientsException : ArgumentException
    {
        public EmptyRecipeIngredientsException() : base("Recipe must contain at least 1 ingredient"){}
    }
}
