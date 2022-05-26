using System;
using System.Collections.Generic;
using RecipeHub.Domain.Model.Enums;

namespace Aggregator.DTO
{
    public class DetailedRecipeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public Category Category { get; set; }
        public List<CommentDto> Comments { get; set; }
        public List<RecipeIngredientDto> RecipeIngredients { get; set; }
        public uint PreparationTime { get; set; }
        public string ImgSrc { get; set; }
        public Guid UserId { get; set; }
    }
}
