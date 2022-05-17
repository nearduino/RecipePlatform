using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Infrastructure.DBO;
using Shouldly;
using Xunit;

namespace RecipeHub.UnitTests.InfrastructureTests
{
    public class RecipeDboTests
    {
        [Fact]
        public void Recipe_dbo_overwrite_ingredients_should_return_seven()
        {
            RecipeDbo recipeDbo = new RecipeDbo
            {
                RecipeIngredientsDbo = new List<RecipeIngredientDbo>(),
                CommentsDbo = new List<CommentDbo>()
            };
            recipeDbo.RecipeIngredientsDbo.Add(new RecipeIngredientDbo{IngredientDboId = 1, Quantity = 1 });
            recipeDbo.RecipeIngredientsDbo.Add(new RecipeIngredientDbo{IngredientDboId = 3, Quantity = 3 });
            recipeDbo.RecipeIngredientsDbo.Add(new RecipeIngredientDbo{IngredientDboId = 5, Quantity = 5 });
            recipeDbo.RecipeIngredientsDbo.Add(new RecipeIngredientDbo{IngredientDboId = 7, Quantity = 6 });
            recipeDbo.RecipeIngredientsDbo.Add(new RecipeIngredientDbo{IngredientDboId = 10, Quantity = 6 });
            recipeDbo.RecipeIngredientsDbo.Add(new RecipeIngredientDbo{IngredientDboId = 12, Quantity = 6 });

            RecipeDbo dbo = new RecipeDbo()
            {
                RecipeIngredientsDbo = new List<RecipeIngredientDbo>(),
                CommentsDbo = new List<CommentDbo>()
            };
            dbo.RecipeIngredientsDbo.Add(new RecipeIngredientDbo { IngredientDboId = 2, Quantity = 10 });
            dbo.RecipeIngredientsDbo.Add(new RecipeIngredientDbo { IngredientDboId = 3, Quantity = 10 });
            dbo.RecipeIngredientsDbo.Add(new RecipeIngredientDbo { IngredientDboId = 5, Quantity = 10 });
            dbo.RecipeIngredientsDbo.Add(new RecipeIngredientDbo { IngredientDboId = 6, Quantity = 10 });
            dbo.RecipeIngredientsDbo.Add(new RecipeIngredientDbo { IngredientDboId = 11, Quantity = 10 });
            dbo.RecipeIngredientsDbo.Add(new RecipeIngredientDbo { IngredientDboId = 13, Quantity = 10 });
            dbo.RecipeIngredientsDbo.Add(new RecipeIngredientDbo { IngredientDboId = 14, Quantity = 10 });
            recipeDbo.Overwrite(dbo);
            recipeDbo.RecipeIngredientsDbo.Count.ShouldBe(7);
            recipeDbo.RecipeIngredientsDbo[1].Quantity.ShouldBe(10);
        }

        [Fact]
        public void Recipe_dbo_overwrite_comments_should_return_seven()
        {
            RecipeDbo recipeDbo = new RecipeDbo
            {
                RecipeIngredientsDbo = new List<RecipeIngredientDbo>(),
                CommentsDbo = new List<CommentDbo>()
            };
            recipeDbo.CommentsDbo.Add(new CommentDbo { Id = 1 });
            recipeDbo.CommentsDbo.Add(new CommentDbo { Id = 2 });
            recipeDbo.CommentsDbo.Add(new CommentDbo { Id = 3 });
            recipeDbo.CommentsDbo.Add(new CommentDbo { Id = 4 });
            recipeDbo.CommentsDbo.Add(new CommentDbo { Id = 5 });
            recipeDbo.CommentsDbo.Add(new CommentDbo { Id = 6 });

            RecipeDbo dbo = new RecipeDbo()
            {
                RecipeIngredientsDbo = new List<RecipeIngredientDbo>(),
                CommentsDbo = new List<CommentDbo>()
            };
            dbo.CommentsDbo.Add(new CommentDbo { Id = 5 });
            dbo.CommentsDbo.Add(new CommentDbo { Id = 6 });
            dbo.CommentsDbo.Add(new CommentDbo { Id = 7 });
            dbo.CommentsDbo.Add(new CommentDbo { Id = 8 });
            dbo.CommentsDbo.Add(new CommentDbo { Id = 9 });
            dbo.CommentsDbo.Add(new CommentDbo { Id = 10 });
            dbo.CommentsDbo.Add(new CommentDbo { Id = 11 });
            recipeDbo.Overwrite(dbo);
            recipeDbo.CommentsDbo.Count.ShouldBe(7);
        }
    }
}
