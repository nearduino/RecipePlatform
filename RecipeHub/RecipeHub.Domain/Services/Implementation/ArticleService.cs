using RecipeHub.Domain.Abstractions;
using RecipeHub.Domain.Exceptions;
using RecipeHub.Domain.InfrasctructureInterfaces;
using RecipeHub.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Domain.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleInfrastructureService _articleInfrastructureService;

        public ArticleService(IArticleInfrastructureService articleInfrastructureService)
        {
            _articleInfrastructureService = articleInfrastructureService;
        }

        public ArticleService()
        {
        }

        public IEnumerable<Article> GetAll()
        {
            return _articleInfrastructureService.GetAll().ToList();
        }

        public void CreateArticle(Article article)
        {
            IEnumerable<Article> articles = _articleInfrastructureService.GetAll();
            if (articles.Contains(article))
            {
                throw new ArticleException($"Article with Id {article.Id} already exists!");
            }
            _articleInfrastructureService.CreateArticle(article);
            Console.WriteLine("Article {0} added.", article.Title);
        }
        public Article ReadArticle(Guid articleId)
        {
            if (_articleInfrastructureService.ReadArticle(articleId) == null)
            {
                throw new ArticleException($"Article with Id {articleId} is not found!");

            }
            Console.WriteLine("Article with Id {0} is opened to read.", articleId);
            return _articleInfrastructureService.ReadArticle(articleId);
        }

        public void UpdateArticle(Article article)
        {
            try
            {
                var art = _articleInfrastructureService.ReadArticle(article.Id);
            }
            catch (InvalidOperationException)
            {
                throw new ArticleException($"Article not found!");
            }
            _articleInfrastructureService.UpdateArticle(article);
            Console.WriteLine("Article with Id {0} updated.", article.Id);
        }

        public void DeleteArticle(Guid articleId)
        {
            if (_articleInfrastructureService.ReadArticle(articleId) == null)
            {
                throw new ArticleException($"Article with Id {articleId} is not found!");
            }
            _articleInfrastructureService.DeleteArticle(articleId);
            Console.WriteLine("Article with Id {0} deleted.", articleId);
        }
    }
}
