using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model.Enums;

namespace RecipeHub.Infrastructure.DBO
{
    public class IngredientDbo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
        public int CaloriesPerUnit { get; set; }

    }
}
