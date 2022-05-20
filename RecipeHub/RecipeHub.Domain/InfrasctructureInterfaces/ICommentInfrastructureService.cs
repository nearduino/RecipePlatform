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
        public IEnumerable<Comment> GetCommentsByIds(IEnumerable<Guid> ids);
        public IEnumerable<Comment> GetCommentsByRecipeId(Guid recipeID);
        /* DB: Old
        public IEnumerable<Comment> GetAll(Guid recipeId);
        public void CreateComment(Guid recipeId, Comment comment);
        public Comment ReadComment(Guid recipeId, Guid id);
        public void UpdateCommnet(Guid recipeId, Comment commnet);
        public void DeleteCommnet(Guid recipeId, Guid id);
        */
    }
}
