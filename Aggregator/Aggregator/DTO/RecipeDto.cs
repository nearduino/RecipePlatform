using System;
using RecipeHub.Domain.Model.Enums;

namespace RecipeHub.API.DTO
{
    public class RecipeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public uint PreparationTime { get; set; }
    }
}
