using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RecipeHub.Domain.Model;
using RecipeHub.Infrastructure.DBO;

namespace RecipeHub.Infrastructure.AutoMapperProfiles
{
    public class AutoMapperRecipe : Profile
    {
        public AutoMapperRecipe()
        {
            CreateMap<RecipeDbo, Recipe>()
                .ForMember(r => r.Comments, opt => opt.MapFrom(src => src.CommentsDbo))
                .ForMember(r => r.RecipeIngredients, opt => opt.MapFrom(src => src.RecipeIngredientsDbo));
            CreateMap<Recipe, RecipeDbo>()
                .ForMember(r => r.CommentsDbo, opt => opt.MapFrom(src => src.Comments))
                .ForMember(r => r.RecipeIngredientsDbo, opt => opt.MapFrom(src => src.RecipeIngredients));
            CreateMap<RecipeDbo, RecipeDbo>();
        }
    }
}
