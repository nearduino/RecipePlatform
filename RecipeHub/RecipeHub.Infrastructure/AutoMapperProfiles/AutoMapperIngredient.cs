using AutoMapper;
using RecipeHub.Domain.Model;
using RecipeHub.Infrastructure.DBO;

namespace RecipeHub.Infrastructure.AutoMapperProfiles
{
    public class AutoMapperIngredient : Profile
    {
        public AutoMapperIngredient()
        {
            CreateMap<RecipeIngredient, RecipeIngredientDbo>()
                .ForMember(r => r.IngredientDbo, opt => opt.MapFrom(src => src.Ingredient))
                .ForMember(r => r.IngredientDboId, opt => opt.MapFrom(src => src.Ingredient.Id));
            CreateMap<RecipeIngredientDbo, RecipeIngredient>()
                //.ForMember(r => r.Ingredient, opt => opt.MapFrom(src => src.IngredientDbo));
                .ForCtorParam("ingredient", opt => opt.MapFrom(src => src.IngredientDbo));
            CreateMap<Ingredient, IngredientDbo>();
            CreateMap<IngredientDbo, Ingredient>()
                .ConstructUsing(i => new Ingredient(i.CaloriesPerUnit, i.Name, i.MeasureUnit));
        }
    }
}
