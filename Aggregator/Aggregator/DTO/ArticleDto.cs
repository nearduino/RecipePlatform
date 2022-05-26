using System;
using System.Collections.Generic;
using RecipeHub.Domain.Model.Enums;

namespace RecipeHub.API.DTO
{
    public class ArticleDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
