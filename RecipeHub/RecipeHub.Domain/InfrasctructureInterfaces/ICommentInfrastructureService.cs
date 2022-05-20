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
        public IEnumerable<Comment> GetAll();
        public void CreateComment(Comment comment);
        public Comment ReadComment(Guid id);
        public void UpdateCommnet(Comment commnet);
        public void DeleteCommnet(Guid id);
    }
}
