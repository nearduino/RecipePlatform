using AutoMapper;
using RecipeHub.API.DTO;
using RecipeHub.Domain.Model;

namespace RecipeHub.API.AutoMappperProfiles
{
    public class AutoMapperComment : Profile
    {
        public AutoMapperComment()
        {
            CreateMap<CommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();
        }
    }
}
