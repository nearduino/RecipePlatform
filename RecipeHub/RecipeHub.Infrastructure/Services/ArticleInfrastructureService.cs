using AutoMapper;
using RecipeHub.Domain.InfrasctructureInterfaces;
using RecipeHub.Domain.Model;
using RecipeHub.Infrastructure.DBO;
using RecipeHub.Infrastructure.Mappers;
using RecipeHub.Infrastructure.Repositories;
using RecipeHub.Infrastructure.Repositories.Enums;
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
        private readonly IMapper _mapper;

        public ArticleInfrastructureService(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public IEnumerable<Article> GetAll()
        {
            List<Article> articles = new List<Article>();
            var articleDbos = _articleRepository.GetAll();
            foreach (var articleDbo in articleDbos)
            {
                articles.Add(_mapper.Map<Article>(articleDbo));
            }
            return articles;
        }

        public void CreateArticle(Article article)
        {
            var dbo = _mapper.Map<ArticleDbo>(article);
            _articleRepository.Add(dbo);
        }

        public Article ReadArticle(Guid id)
        {
            var dbo = _articleRepository.GetById(id);
            var article = _mapper.Map<Article>(dbo);
            return article;
        }

        public void UpdateArticle(Article article)
        {
            var fromDatabase = _articleRepository.GetById(article.Id);
            var dbo = _mapper.Map<ArticleDbo>(article);
            _mapper.Map(dbo, fromDatabase);
            _articleRepository.Update(fromDatabase);
        }

        public void DeleteArticle(Guid id)
        {
            _articleRepository.Delete(_articleRepository.GetById(id));
        }
    }
}
