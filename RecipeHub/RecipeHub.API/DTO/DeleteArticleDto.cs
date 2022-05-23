using System;

namespace RecipeHub.API.DTO
{
    public class DeleteArticleDto
    {
        public Guid UserId { get; set; }
        public Guid ArticleId { get; set; }
    }
}
