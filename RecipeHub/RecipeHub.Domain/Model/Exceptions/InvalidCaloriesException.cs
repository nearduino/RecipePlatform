using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Model.Exceptions
{
    public class InvalidCaloriesException : ArgumentException
    {
        public InvalidCaloriesException() : base("Calories must be greater than zero")
        {

        }
    }
}
