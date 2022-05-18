using RecipeHub.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.InfrasctructureInterfaces
{
    public interface IArticleInfrastructureService
    {
        public IEnumerable<Article> GetAll();
        public void CreateArticle(Article article);
        public Article ReadArticle(Guid id);
        public void UpdateArticle(Article article);
        public void DeleteArticle(Guid id);
    }
}
