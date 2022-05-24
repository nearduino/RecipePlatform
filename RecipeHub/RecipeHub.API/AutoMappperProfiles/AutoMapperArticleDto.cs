using AutoMapper;
using RecipeHub.API.DTO;
using RecipeHub.Domain.Model;

namespace RecipeHub.API.AutoMappperProfiles
{
    public class AutoMapperArticleDto : Profile
    {
        public AutoMapperArticleDto()
        {
            CreateMap<ArticleDto, Article>();
            CreateMap<Article, ArticleDto>();
            CreateMap<UpdateArticleDto, Article>();
        }
    }
}
