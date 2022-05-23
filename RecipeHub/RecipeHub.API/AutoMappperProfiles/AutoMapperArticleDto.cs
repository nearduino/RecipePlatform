using AutoMapper;
using RecipeHub.API.DTO;
using RecipeHub.Domain.Model;

namespace RecipeHub.API.AutoMappperProfiles
{
    public class AutoMapperArticleDto : Profile
    {
        public AutoMapperArticleDto()
        {
            CreateMap<ArticleDto, Article>()
                .ConstructUsing(a => new Article(a.Id, a.UserId, a.Title, a.Text));
            CreateMap<Article, ArticleDto>();
        }
    }
}
