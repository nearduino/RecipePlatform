using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model;
using RecipeHub.Domain.Model.Enums;

namespace RecipeHub.Infrastructure.DBO
{
    public class RecipeDbo
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public List<CommentDbo> CommentsDbo { get; set; }
        public List<RecipeIngredientDbo> RecipeIngredientsDbo { get; set; }
        public uint PreparationTime { get; set; }
        public string ImgSrc { get; set; }
    }
}
