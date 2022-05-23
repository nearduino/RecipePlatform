using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Infrastructure.DBO;
using RecipeHub.Infrastructure.EfStructures;
using RecipeHub.Infrastructure.Repositories.Base;
using RecipeHub.Infrastructure.Repositories.Enums;

namespace RecipeHub.Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Guid, CommentDbo>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }

        public CommentDbo getById(Guid id, FetchType fetchType = FetchType.Lazy)
        {
            var comments = GetAll();
            if (fetchType == FetchType.Eager) { }
            return comments.First(c => c.Id == id);
        }

        public IEnumerable<CommentDbo> GetByIds(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CommentDbo> GetByRecipeId(Guid recipeId)
        {
            throw new NotImplementedException();
        }
    }
}
