using RecipeHub.Domain.Abstractions;
using RecipeHub.Domain.Exceptions;
using RecipeHub.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Implementations
{
    public class ArticleLogic : IArticleLogic
    {
        private readonly Dictionary<int, Article> Articles = new Dictionary<int, Article>();

        public void CreateArticle(int articleId, Article article)
        {
            if (Articles.ContainsKey(articleId))
            {
                throw new ArticleException($"Article with Id {articleId} already exists!");
            }
            Articles.Add(articleId, article);
            Console.WriteLine("Article {0} added.", article.Title);
        }
        public Article ReadArticle(int articleId)
        {
            if (!Articles.ContainsKey(articleId))
            {
                throw new ArticleException($"Article with Id {articleId} is not found!");

            }
            Console.WriteLine("Article with Id {0} is opened to read.", articleId);
            return Articles[articleId];
        }

        public void UpdateArticle(int articleId, Article article)
        {
            if (!Articles.ContainsKey(articleId))
            {
                throw new ArticleException($"Article with Id {articleId} is not found!");

            }
            Articles[articleId].Title = article.Title;
            Articles[articleId].Text = article.Text;
            Console.WriteLine("Article with Id {0} updated.", articleId);
        }

        public void DeleteArticle(int articleId)
        {
            if (!Articles.ContainsKey(articleId))
            {
                throw new ArticleException($"Article with Id {articleId} is not found!");
            }
            Articles.Remove(articleId);
            Console.WriteLine("Article with Id {0} deleted.", articleId);
        }

    }
}
