using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model.Enums;

namespace RecipeHub.Domain.Model
{
    public class Ingredient
    {
        public int Calories { get; set; }
            public string Name { get; set; }
        public int Quantity { get; set; }
        public MeasureUnit MeasureUnit { get; set; }

    }
}
