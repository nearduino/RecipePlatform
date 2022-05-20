using AutoMapper;
using RecipeHub.Domain.Model;
using RecipeHub.Infrastructure.DBO;

namespace RecipeHub.Infrastructure.AutoMapperProfiles
{
    public class AutoMapperComment : Profile
    {
        public AutoMapperComment()
        {
            CreateMap<CommentDbo, Comment>();
            CreateMap<Comment, CommentDbo>();
        }
    }
}
