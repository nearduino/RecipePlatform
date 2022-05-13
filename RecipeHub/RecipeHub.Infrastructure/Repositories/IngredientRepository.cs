using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Infrastructure.DBO;
using RecipeHub.Infrastructure.EfStructures;
using RecipeHub.Infrastructure.Repositories.Base;
using RecipeHub.Infrastructure.Repositories.Enums;

namespace RecipeHub.Infrastructure.Repositories
{
    public class IngredientRepository : BaseRepository<int, IngredientDbo>, IIngredientRepository
    {
        public IngredientRepository(AppDbContext context) : base(context)
        {
        }
        public IngredientDbo getById(int id)
        {
            return GetAll().First(i => i.Id == id);
        }
    }
}
