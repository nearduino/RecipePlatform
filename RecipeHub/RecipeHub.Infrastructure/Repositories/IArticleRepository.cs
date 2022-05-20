using RecipeHub.Infrastructure.DBO;
using RecipeHub.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Infrastructure.Repositories
{
    public interface IArticleRepository : IBaseRepository<int, ArticleDbo>
    {
        public ArticleDbo GetById(Guid id);
    }
}
