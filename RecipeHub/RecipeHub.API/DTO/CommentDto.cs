using System;

namespace RecipeHub.API.DTO
{
    public class CommentDto
    {
        public Guid RecipeId { get; set; }
        public uint Rating { get; set; }
        public string Text { get; set; }
    }
}
