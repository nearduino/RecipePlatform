using RecipeHub.Domain.InfrasctructureInterfaces;
using RecipeHub.Domain.Model;
using RecipeHub.Infrastructure.Mappers;
using RecipeHub.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Infrastructure.Services
{
    public class ArticleInfrastructureService : IArticleInfrastructureService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleInfrastructureService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public IEnumerable<Article> GetAll()
        {
            List<Article> articles = new List<Article>();
            var articleDbos = _articleRepository.GetAll();
            foreach (var articleDbo in articleDbos)
            {
                articles.Add(Mapper.Map(articleDbo));
            }
            return articles;
        }

        public void CreateArticle(Article article)
        {
            throw new NotImplementedException();
        }

        public Article ReadArticle(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateArticle(Article article)
        {
            throw new NotImplementedException();
        }

        public void DeleteArticle(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
