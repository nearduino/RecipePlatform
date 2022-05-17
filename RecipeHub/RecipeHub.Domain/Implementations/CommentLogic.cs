using RecipeHub.Domain.Abstractions;
using RecipeHub.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Implementations
{
    public class CommentLogic : ICommentLogic
    {
        public void CreateComment(int userId, int recipeId, Comment comment)
        {

        }

        public Comment ReadComment(int userId, int recipeId)
        {
            throw new NotImplementedException();
        }

        public void UpdateComment(int userId, int recipeId, Comment comment)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(int userId, int recipeId)
        {
            throw new NotImplementedException();
        }
    }
}
