using AutoMapper;
using RecipeHub.API.DTO;
using RecipeHub.Domain.Model;

namespace RecipeHub.API.AutoMappperProfiles
{
    public class AutoMapperRecipeDto : Profile
    {
        public AutoMapperRecipeDto()
        {
            CreateMap<Recipe, RecipeDto>();
        }
    }
}
