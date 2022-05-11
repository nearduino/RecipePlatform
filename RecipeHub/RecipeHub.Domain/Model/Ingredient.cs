using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model.Enums;
using RecipeHub.Domain.Model.Exceptions;

namespace RecipeHub.Domain.Model
{
    public class Ingredient
    {
        public int Calories { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public MeasureUnit MeasureUnit { get; set; }

        public Ingredient(int calories, string name, int quantity, MeasureUnit measureUnit)
        {
            Calories = calories;
            Name = name;
            Quantity = quantity;
            MeasureUnit = measureUnit;
            Validate();
        }

        private void Validate()
        {
            if (Quantity <= 0) throw new InvalidIngredientQuantityException();
            if (Calories <= 0) throw new InvalidCaloriesException();
        }

    }
}
