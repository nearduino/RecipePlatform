using RecipeHub.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Abstractions
{
    public interface ICommentLogic
    {
        public void CreateComment(int userId, int recipeId, Comment comment);
        public Comment ReadComment(int userId, int recipeId);
        public void UpdateComment(int userId, int recipeId, Comment comment);
        public void DeleteComment(int userId, int recipeId);
    }
}
