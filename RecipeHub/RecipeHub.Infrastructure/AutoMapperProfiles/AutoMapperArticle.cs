using AutoMapper;
using RecipeHub.Domain.Model;
using RecipeHub.Infrastructure.DBO;

namespace RecipeHub.API.AutoMappperProfiles
{
    public class AutoMapperArticle : Profile
    {
        public AutoMapperArticle()
        {
            CreateMap<ArticleDbo, Article>()
                .ConstructUsing(a => new Article(a.Id, a.UserId, a.Title, a.Text));
            CreateMap<Article, ArticleDbo>();
            CreateMap<ArticleDbo, ArticleDbo>();
        }
    }
}
