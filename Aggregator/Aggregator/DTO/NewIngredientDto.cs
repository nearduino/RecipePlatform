using RecipeHub.Domain.Model.Enums;

namespace Aggregator.DTO
{
    public class NewIngredientDto
    {
        public int CaloriesPerUnit { get; set; }
        public string Name { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
    }
}
