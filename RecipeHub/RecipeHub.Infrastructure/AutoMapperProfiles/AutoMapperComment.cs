using AutoMapper;
using RecipeHub.Domain.Model;
using RecipeHub.Infrastructure.DBO;

namespace RecipeHub.Infrastructure.AutoMapperProfiles
{
    public class AutoMapperComment : Profile
    {
        public AutoMapperComment()
        {
            CreateMap<CommentDbo, Comment>()
                .ConstructUsing(c => new Comment(c.Id, c.Rating, c.Text));
            CreateMap<Comment, CommentDbo>();
            CreateMap<CommentDbo, CommentDbo>().ForMember(c => c.RecipeDboId, opt => opt.Ignore());
        }
    }
}
