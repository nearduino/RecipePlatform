using System;

namespace RecipeHub.API.DTO
{
    public class UpdateCommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public uint Rating { get; set; }
    }
}
