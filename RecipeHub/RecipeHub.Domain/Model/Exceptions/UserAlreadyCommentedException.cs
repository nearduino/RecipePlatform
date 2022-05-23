using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Model.Exceptions
{
    public class UserAlreadyCommentedException : Exception
    {
        public UserAlreadyCommentedException() : base("User has already commented on this recipe")
        {

        }
    }
}
