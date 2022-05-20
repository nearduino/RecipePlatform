using System;

namespace RecipeHub.API.DTO
{
    public class RecipeIngredientDto
    {
        public Guid IngredientId { get; set; }
        public int Quantity { get; set; }
    }
}
