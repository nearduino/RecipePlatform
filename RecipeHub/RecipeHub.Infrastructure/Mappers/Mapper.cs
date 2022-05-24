using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model;
using RecipeHub.Infrastructure.DBO;

namespace RecipeHub.Infrastructure.Mappers
{
    public class Mapper
    {
        public static Recipe Map(RecipeDbo dbo)
        {
            if(dbo == null)return null;
            List<Comment> comments = new List<Comment>();
            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
            if (dbo.CommentsDbo != null)
            {
                foreach (var comment in dbo.CommentsDbo) 
                    comments.Add(Map(comment));
            }
            if (dbo.RecipeIngredientsDbo != null)
            {
                foreach (var recipeIngredient in dbo.RecipeIngredientsDbo) 
                    recipeIngredients.Add(Map(recipeIngredient));
            }
            return new Recipe(dbo.Category,
                dbo.Name,
                dbo.Description,
                dbo.Instructions,
                dbo.PreparationTime,
                recipeIngredients,
                comments,
                dbo.UserId,
                dbo.Id);
        }

        public static RecipeDbo Map(Recipe recipe)
        {
            if(recipe == null)return null;
            List<CommentDbo> commentDbos = new List<CommentDbo>();
            foreach (var comment in recipe.Comments) 
                commentDbos.Add(Map(comment));
            List<RecipeIngredientDbo> recipeIngredientDbos = new List<RecipeIngredientDbo>();
            foreach (var recipeIngredient in recipe.RecipeIngredients)
                recipeIngredientDbos.Add(Map(recipeIngredient));

            RecipeDbo dbo = new RecipeDbo
            {
                Category = recipe.Category,
                Description = recipe.Description,
                ImgSrc = recipe.ImgSrc,
                Name = recipe.Name,
                PreparationTime = recipe.PreparationTime,
                Instructions = recipe.Instructions,
                CommentsDbo = commentDbos,
                RecipeIngredientsDbo = recipeIngredientDbos,
                UserId = recipe.UserId,
            };
            return dbo;
        }

        public static Ingredient Map(IngredientDbo dbo)
        {
            if(dbo == null)return null;
            return new Ingredient(dbo.CaloriesPerUnit,
                dbo.Name, dbo.MeasureUnit,
                dbo.Id);
        }

        public static IngredientDbo Map(Ingredient ingredient)
        {
            if(ingredient == null)return null;
            return new IngredientDbo
            {
                CaloriesPerUnit = ingredient.CaloriesPerUnit,
                Id = ingredient.Id,
                MeasureUnit = ingredient.MeasureUnit,
                Name = ingredient.Name
            };
        }

        public static Comment Map(CommentDbo dbo)
        {
            if(dbo ==null)return null;
            return new Comment(dbo.Id, dbo.Rating, dbo.Text);
        }

        public static CommentDbo Map(Comment comment)
        {
            if(comment == null)return null;
            return new CommentDbo
            {
                Id = comment.Id,
                Rating = comment.Rating,
                Text = comment.Text,
            };
        }

        public static Article Map(ArticleDbo dbo)
        {
            if (dbo == null) return null;
            return new Article(dbo.Id, dbo.UserId, dbo.Title, dbo.Text);
        }

        public static ArticleDbo Map(Article article)
        {
            if (article == null) return null;
            return new ArticleDbo
            {
                Id = article.Id,
                Title = article.Title,
                Text = article.Text
            };
        }

        public static RecipeIngredientDbo Map(RecipeIngredient recipeIngredient)
        {
            if(recipeIngredient == null)return null;
            return new RecipeIngredientDbo
            {
                IngredientDboId = recipeIngredient.Ingredient.Id,
                Quantity = recipeIngredient.Quantity
            };
        }

        public static RecipeIngredient Map(RecipeIngredientDbo dbo)
        {
            if(dbo == null)return null;
            return new RecipeIngredient(dbo.Quantity, Map(dbo.IngredientDbo));
        }
    }
}
