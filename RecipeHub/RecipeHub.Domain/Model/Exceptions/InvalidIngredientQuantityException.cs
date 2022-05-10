using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Model.Exceptions
{
    public class InvalidIngredientQuantityException : ArgumentException
    {
        public InvalidIngredientQuantityException() : base("Ingredient quantity must be greater than 0!")
        {

        }
    }
}
