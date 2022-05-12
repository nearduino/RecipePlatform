using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Infrastructure.DBO
{
    public class RecipeIngredientDbo
    {
        public IngredientDbo Ingredient { get; set; }
        public int Quantity { get; set; }
    }
}
