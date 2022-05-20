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
    public interface IRecipeRepository : IBaseRepository<int, RecipeDbo>
    {
        public RecipeDbo GetById(Guid id, FetchType fetchType = FetchType.Lazy);
    }
}
