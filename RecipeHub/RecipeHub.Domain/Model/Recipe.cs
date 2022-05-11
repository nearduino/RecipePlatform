using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model.Enums;

namespace RecipeHub.Domain.Model
{
    public class Recipe
    {
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
        public int Instructions { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set; }
        public int PreparationTime { get; set; }

        public string ImgSrc { get; set; }

    }
}
