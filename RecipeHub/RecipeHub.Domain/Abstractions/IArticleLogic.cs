using RecipeHub.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Abstractions
{
    public interface IArticleLogic
    {
        public void CreateArticle(int articleId, Article article);
        public Article ReadArticle(int articleId);
        public void UpdateArticle(int articleId, Article article);
        public void DeleteArticle(int articleId);
    }
}
