using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Infrastructure.DBO;
using RecipeHub.Infrastructure.Repositories.Base;
using RecipeHub.Infrastructure.Repositories.Enums;

namespace RecipeHub.Infrastructure.Repositories
{
    public interface ICommentRepository : IBaseRepository<Guid, CommentDbo>
    {
        public CommentDbo getById(Guid id, FetchType fetchType = FetchType.Lazy);
        public IEnumerable<CommentDbo> GetByIds(IEnumerable<Guid> ids);
        public IEnumerable<CommentDbo> GetByRecipeId(Guid recipeId);
    }
}
