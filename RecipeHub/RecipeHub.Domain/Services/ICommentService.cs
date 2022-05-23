using RecipeHub.Domain.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Abstractions
{
    public interface ICommentService
    {
        public void CreateComment(Comment comment, Guid recipeId);
        public IEnumerable<Comment> ReadComments(Guid userId, Guid recipeId);
        public void UpdateComment(Comment comment);
        public void DeleteComment(Guid commentId);
        public IEnumerable GetAll();
        public Comment GetById(Guid commentId);
    }
}
