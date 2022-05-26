using System;
using RecipeHub.Domain.Model.Enums;

namespace Aggregator.DTO
{
    public class IngredientDto
    {
        public Guid Id { get; set; }
        public int CaloriesPerUnit { get; set; }
        public string Name { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
    }
}
