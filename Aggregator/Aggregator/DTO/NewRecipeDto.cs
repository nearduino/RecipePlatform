using RecipeHub.Domain.Model.Enums;
using System;
using System.Collections.Generic;


namespace RecipeHub.API.DTO
{
    public class NewRecipeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public Category Category { get; set; }
        public uint PreparationTime { get; set; }
        public IEnumerable<RecipeIngredientDto> Ingredients { get; set; }
    }
}
