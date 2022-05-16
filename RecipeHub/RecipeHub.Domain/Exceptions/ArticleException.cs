using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Exceptions
{
    public class ArticleException : Exception
    {
        public ArticleException() { }

        public ArticleException(string message)
            : base(message) { }
    }
}
