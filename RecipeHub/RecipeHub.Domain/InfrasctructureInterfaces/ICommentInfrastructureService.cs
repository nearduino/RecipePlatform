using RecipeHub.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.InfrasctructureInterfaces
{
    public interface ICommentInfrastructureService
    {
        public Comment GetById(Guid commentId);
        public void CreateComment(Comment comment, Guid recipeId);
        public IEnumerable<Comment> GetCommentsByIds(IEnumerable<Guid> ids);
        public IEnumerable<Comment> GetCommentsByRecipeId(Guid recipeID);
        public IEnumerable<Comment> GetAll();
        void Update(Comment comment);
        void Delete(Guid commentId);
    }
}
