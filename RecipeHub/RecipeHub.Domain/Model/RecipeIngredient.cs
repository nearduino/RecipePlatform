using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model.Exceptions;

namespace RecipeHub.Domain.Model
{
    public class RecipeIngredient
    {
        public int Quantity { get; private set; }
        public Ingredient Ingredient { get; private set; }

        private RecipeIngredient(){}

        public RecipeIngredient(int quantity, Ingredient ingredient)
        {
            Quantity = quantity;
            Ingredient = ingredient;
            Validate();
        }

        private void Validate()
        {
            if (Quantity <= 0) throw new InvalidIngredientQuantityException();
            if (Ingredient == null) throw new ArgumentException("Ingredient is null");
        }

        public int CalculateCalories()
        {
            return Quantity * Ingredient.CaloriesPerUnit;
        }
    }
}
