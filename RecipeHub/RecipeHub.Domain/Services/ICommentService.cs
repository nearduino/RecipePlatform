using RecipeHub.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Abstractions
{
    public interface ICommentService
    {
        public void CreateComment(Guid userId, Guid recipeId, Comment comment);
        public IEnumerable<Comment> ReadComments(Guid userId, Guid recipeId);
        public void UpdateComment(Guid userId, Guid recipeId, Comment comment);
        public void DeleteComment(Guid userId, Guid recipeId);
    }
}
