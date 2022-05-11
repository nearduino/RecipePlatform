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
        public static bool CheckName(string name)
        {
            string namePattern = @"([A-Z]{1}[a-z]+)(([ ]([A-Z]?[a-z]+))|([ ][A-Z]))*";
            return Regex.IsMatch(name, namePattern);
        }
    }
}
