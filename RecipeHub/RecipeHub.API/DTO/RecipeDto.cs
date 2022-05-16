﻿using System.Collections.Generic;
using RecipeHub.Domain.Model.Enums;

namespace RecipeHub.API.DTO
{
    public class RecipeDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public Category Category { get; set; }
        public uint PreparationTime { get; set; }
        public IEnumerable<RecipeIngredientDto> Ingredients { get; set; }
    }
}