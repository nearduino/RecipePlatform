using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Model.Exceptions
{
    public class InvalidNameException : ArgumentException
    {
        public InvalidNameException() : base(
            "Name should contain only A-Z and a-z characters, single letter words must be upper case.")
        {

        }
    }
}
