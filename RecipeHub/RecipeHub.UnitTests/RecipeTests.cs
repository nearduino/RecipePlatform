using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model;
using RecipeHub.Domain.Model.Enums;
using Shouldly;
using Xunit;

namespace RecipeHub.UnitTests
{
    public class RecipeTests
    {
        [Theory]
        [MemberData(nameof(GetRecipeRatingData))]
        public void Recipe_rating(List<Comment> comments, double shouldBe)
        {
            Recipe recipe = new Recipe(Category.Breakfast, "Test rec", "desc", "instr", 20,
                new List<RecipeIngredient>(), comments);
            recipe.CalculateRecipeRating().ShouldBe(shouldBe);
        }

        public static IEnumerable<object[]> GetRecipeRatingData()
        {
            List<Comment> comments = new List<Comment>();
            comments.Add(new Comment(1, "Text"));
            comments.Add(new Comment(2, "Text"));
            comments.Add(new Comment(3, "Text"));
            comments.Add(new Comment(4, "Text"));
            comments.Add(new Comment(5, "Text"));
            comments.Add(new Comment(6, "Text"));
            comments.Add(new Comment(7, "Text"));
            comments.Add(new Comment(8, "Text"));
            comments.Add(new Comment(9, "Text"));
            comments.Add(new Comment(10, "Text"));
            List<object[]> retVal = new List<object[]>();
            retVal.Add(new object[]
            {
                new List<Comment>{comments[9]},
                10.0
            });
            retVal.Add(new object[]
            {
                new List<Comment>{comments[9], comments[0]},
                5.5
            });
            retVal.Add(new object[]
            {
                new List<Comment>{comments[9], comments[3]},
                7
            });
            retVal.Add(new object[]
            {
                new List<Comment>{comments[9], comments[3], comments[0], comments[9], comments[7]},
                6.6
            });
            return retVal;
        }
    }
}
