using System;

namespace RecipeHub.API.DTO
{
    public class NewRecipeIngredientDto
    {
        public Guid IngredientId { get; set; }
        public int Quantity { get; set; }
    }
}
