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
    public class CommentRepository : BaseRepository<int, CommentDbo>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }

        public CommentDbo getById(int id, LoadType loadType = LoadType.Lazy)
        {
            var set = GetAll();
            if (loadType == LoadType.Eager) { } //return set.Include(c => c.Comments).First(c => c.Id == id);
            return set.First(c => c.Id == id);
        }
    }
}
