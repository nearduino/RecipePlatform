using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model;
using RecipeHub.Domain.Model.Enums;
using RecipeHub.Domain.Model.Exceptions;
using Shouldly;
using Xunit;

namespace RecipeHub.xUnitTests
{
    public class RecipeTests
    {
        [Theory]
        [MemberData(nameof(GetRecipeRatingData))]
        public void Recipe_rating(List<Comment> comments, double shouldBe, bool shouldThrowExcpetion)
        {
            Guid id1 = new Guid();
            var ingr = new Ingredient(2, "Testo", MeasureUnit.g, id1);
            Guid userId1 = new Guid();
            Recipe recipe = new Recipe(Category.Breakfast, "Test rec", "desc", "instr", 20,
                new List<RecipeIngredient>{new RecipeIngredient(2, ingr)}, comments, userId1, id1);
            bool exceptionThrown = false;
            try
            {
                recipe.CalculateRecipeRating().ShouldBe(shouldBe);
            }
            catch (RecipeRatingCalculationException)
            {
                exceptionThrown = true;
            }
            exceptionThrown.ShouldBe(shouldThrowExcpetion);
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
                10.0, false
            });
            retVal.Add(new object[]
            {
                new List<Comment>{comments[9], comments[0]},
                5.5, false
            });
            retVal.Add(new object[]
            {
                new List<Comment>{comments[9], comments[3]},
                7, false
            });
            retVal.Add(new object[]
            {
                new List<Comment>{comments[9], comments[3], comments[0], comments[9], comments[7]},
                6.6, false
            });
            retVal.Add(new object[]
            {
                new List<Comment>{},
                0.0, true
            });
            return retVal;
        }
    }
}
