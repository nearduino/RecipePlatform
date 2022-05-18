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
    public class IngredientRepository : BaseRepository<Guid, IngredientDbo>, IIngredientRepository
    {
        public IngredientRepository(AppDbContext context) : base(context)
        {
        }
        public IngredientDbo GetById(Guid id)
        {
            return GetAll().First(i => i.Id == id);
        }

        public IEnumerable<IngredientDbo> GetByIds(IEnumerable<Guid> ids)
        {
            var Ingredients = GetAll();
            return Ingredients.Where(i => ids.Contains(i.Id));
        }
    }
}
