using RecipeHub.Infrastructure.DBO;
using RecipeHub.Infrastructure.EfStructures;
using RecipeHub.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Infrastructure.Repositories.DboRepos
{
    public class ArticleRepository : BaseRepository<int, ArticleDbo>, IArticleRepository
    {
        public ArticleRepository(AppDbContext context) : base(context)
        {
        }

        public ArticleDbo GetById(Guid id)
        {
            return GetAll().First(a => a.Id == id);
        }
    }
}
