using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Model.Exceptions
{
    public class InvalidRatingException : ArgumentException
    {
        public InvalidRatingException() : base("Rating out of range. Valid rating is from 1 to 10")
        {

        }
    }
}
