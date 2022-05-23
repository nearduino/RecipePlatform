using System;
using System.Collections.Generic;
using RecipeHub.Domain.Model.Enums;

namespace RecipeHub.API.DTO
{
    public class UpdateArticleDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
