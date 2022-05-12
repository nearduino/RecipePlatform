using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Infrastructure.DBO;
using RecipeHub.Infrastructure.EfStructures;
using RecipeHub.Infrastructure.Repositories.Base;
using RecipeHub.Infrastructure.Repositories.Enums;

namespace RecipeHub.Infrastructure.Repositories
{
    public class RecipeRepository : BaseRepository<int, RecipeDbo>, IRecipeRepository
    {
        public RecipeRepository(AppDbContext context) : base(context)
        {
        }
        public RecipeDbo GetById(int id, LoadType loadType)
        {
            var set = GetAll();
            if (loadType == LoadType.Eager) return set.Include(r => r.CommentsDbo).First(r => r.Id == id);
            return set.First(r => r.Id == id);
        }
    }
}
