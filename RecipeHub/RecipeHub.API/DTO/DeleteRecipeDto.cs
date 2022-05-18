using System;

namespace RecipeHub.API.DTO
{
    public class DeleteRecipeDto
    {
        public Guid UserId { get; set; }
        public Guid RecipeId { get; set; }
    }
}
