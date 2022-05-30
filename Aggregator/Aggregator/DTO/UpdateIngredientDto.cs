using RecipeHub.Domain.Model.Enums;
using System;

namespace Aggregator.DTO
{
    public class UpdateIngredientDto
    {
        public Guid Id { get; set; }
        public int CaloriesPerUnit { get; set; }
        public string Name { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
    }
}
