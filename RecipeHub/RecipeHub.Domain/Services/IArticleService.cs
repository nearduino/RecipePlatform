using RecipeHub.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Abstractions
{
    public interface IArticleService
    {
        public IEnumerable<Article> GetAll();
        public void CreateArticle(Article article);
        public Article ReadArticle(Guid articleId);
        public void UpdateArticle(Article article);
        public void DeleteArticle(Guid articleId);
    }
}
