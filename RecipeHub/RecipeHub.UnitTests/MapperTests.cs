using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model;
using RecipeHub.Domain.Model.Enums;
using RecipeHub.Infrastructure.DBO;
using RecipeHub.UnitTests.Base;
using Shouldly;
using Xunit;

namespace RecipeHub.UnitTests
{
    public class MapperTests : BaseTest
    {
        public MapperTests(BaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void Test_recipe_to_recipeDbo_count_should_be_one()
        {
            Comment comment = new Comment(1, 2, "Sdasd");
            Ingredient ingr = new Ingredient(1, "Asdasdas", MeasureUnit.g, 1);
            RecipeIngredient recipeIngr = new RecipeIngredient(2, ingr);
            Recipe recipe = new Recipe(0, "Daj", "dasdas", "dsda", 20, new List<RecipeIngredient>() { recipeIngr },
                new List<Comment>() { comment }, 2, 1);
            var dbo = _mapper.Map<RecipeDbo>(recipe);
            dbo.RecipeIngredientsDbo.Count.ShouldBe(1);
            dbo.CommentsDbo.Count.ShouldBe(1);
            dbo.RecipeIngredientsDbo[0].IngredientDboId.ShouldBe(1);
        }

        [Fact]
        public void Test_recipeDbo_to_recipe_count_should_be_one()
        {
            RecipeDbo dbo = new RecipeDbo
            {
                Name = "Burek",
                Category = 0,
                CommentsDbo = new List<CommentDbo>
                {
                    new CommentDbo
                    {
                        Id = 1,
                        Rating = 2,
                        Text = "dsadsa"
                    }
                },
                Description = "asdadsd",
                Id = 2,
                ImgSrc = "",
                Instructions = "sdas",
                PreparationTime = 2,
                RecipeIngredientsDbo = new List<RecipeIngredientDbo>
                {
                    new RecipeIngredientDbo
                    {
                        IngredientDbo = new IngredientDbo
                        {
                            CaloriesPerUnit = 1,
                            Id = 1,
                            MeasureUnit = 0,
                            Name = "sdasdasd"
                        },
                        IngredientDboId = 1,
                        Quantity = 2
                    }
                },
                UserId = 1
            };
            var recipe = _mapper.Map<Recipe>(dbo);
            recipe.RecipeIngredients.Count.ShouldBe(1);
            recipe.Comments.Count.ShouldBe(1);
        }

        [Fact]
        public void Test_recipeIngredient_to_recipeIngredientDbo()
        {
            RecipeIngredient recipeIngredient = new RecipeIngredient(1, new Ingredient(1, "Ads", 0, 5));
            var dbo = _mapper.Map<RecipeIngredientDbo>(recipeIngredient);
            dbo.IngredientDboId.ShouldBe(5);
            dbo.IngredientDbo.Id.ShouldBe(5);
            dbo.IngredientDbo.CaloriesPerUnit.ShouldBe(1);
        }
    }
}
