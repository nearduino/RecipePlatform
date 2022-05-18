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
        public RecipeDbo GetById(Guid id, FetchType fetchType = FetchType.Lazy)
        {
            var recipes = GetAll();
            if (fetchType == FetchType.Eager) return recipes.
                Include(r => r.CommentsDbo).
                Include(r => r.RecipeIngredientsDbo).
                ThenInclude(r => r.IngredientDbo).
                First(r => r.Id == id);
            return recipes.First(r => r.Id == id);
        }
    }
}
