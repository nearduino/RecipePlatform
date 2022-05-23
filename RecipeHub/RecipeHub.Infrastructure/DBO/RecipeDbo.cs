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
        public Guid Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public List<CommentDbo> CommentsDbo { get; set; }
        public List<RecipeIngredientDbo> RecipeIngredientsDbo { get; set; }
        public uint PreparationTime { get; set; }
        public string ImgSrc { get; set; }
        public Guid UserId { get; set; }

        //Objanjenje (na srpskom radi lakseg razumevanja): Entity framework ne dozvoljava tracking vise entiteta sa
        //istim kljucem (Id), Detachovanje nije uspelo tako da je ideja da se dobavi entitet iz baze i njegova polja 
        //overwrituju bez pravljenja novih objekata, i da se on onda prosledi u update
        public void Overwrite(RecipeDbo dbo)
        {
            Category = dbo.Category;
            Name = dbo.Name;
            Description = dbo.Description;
            Instructions = dbo.Instructions;
            PreparationTime = dbo.PreparationTime;
            ImgSrc = dbo.ImgSrc;
            UserId = dbo.UserId;
            AddNewComment(dbo);
            RemoveComments(dbo);
            AddNewIngredients(dbo);
            RemoveIngredients(dbo);
        }

        private void RemoveComments(RecipeDbo dbo)
        {
            for (int i = 0; i < CommentsDbo.Count; i++)
            {
                bool removed = true;
                for (int j = 0; j < dbo.CommentsDbo.Count; j++)
                {
                    if (CommentsDbo[i].Id == dbo.CommentsDbo[j].Id) removed = false;
                }

                if (removed)
                {
                    CommentsDbo.RemoveAt(i);
                    i--;
                }
            }
        }
        // D: Imamo add new comment, treba dodati remove comment
        private void AddNewComment(RecipeDbo dbo)
        {
            for (int i = 0; i < dbo.CommentsDbo.Count; i++)
            {
                bool alreadyInRecipe = false;
                for (int j = 0; j < CommentsDbo.Count; j++)
                {
                    if (dbo.CommentsDbo[i].Id == CommentsDbo[j].Id)
                        alreadyInRecipe = true;
                }
                if (!alreadyInRecipe)
                    CommentsDbo.Add(dbo.CommentsDbo[i]);
            }
        }

        private void AddNewIngredients(RecipeDbo dbo)
        {
            for (int i = 0; i < dbo.RecipeIngredientsDbo.Count; i++)
            {
                bool alreadyInRecipe = false;
                for (int j = 0; j < RecipeIngredientsDbo.Count; j++)
                {
                    if (dbo.RecipeIngredientsDbo[i].IngredientDboId == RecipeIngredientsDbo[j].IngredientDboId)
                    {
                        RecipeIngredientsDbo[j].Quantity = dbo.RecipeIngredientsDbo[i].Quantity;
                        alreadyInRecipe = true;
                    }
                }
                if (!alreadyInRecipe)
                {
                    RecipeIngredientsDbo.Add(dbo.RecipeIngredientsDbo[i]);
                }
            }
        }
        private void RemoveIngredients(RecipeDbo dbo)
        {
            for (int i = 0; i < RecipeIngredientsDbo.Count; i++)
            {
                bool removed = true;
                for (int j = 0; j < dbo.RecipeIngredientsDbo.Count; j++)
                {
                    if (dbo.RecipeIngredientsDbo[j].IngredientDboId == RecipeIngredientsDbo[i].IngredientDboId) 
                        removed = false;
                }

                if (removed)
                {
                    RecipeIngredientsDbo.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
