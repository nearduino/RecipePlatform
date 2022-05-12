using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Utilities
{
    public class TextValidator
    {
        private static string NamePattern => @"^([A-Z][a-z]+)(([ ][A-Z]?[a-z]+)|([ ][a-z]+)|([ ][A-Z]))*$";
        public static bool CheckName(string name)
        {
            return Regex.Match(name, NamePattern).Success;
        }
    }
}
