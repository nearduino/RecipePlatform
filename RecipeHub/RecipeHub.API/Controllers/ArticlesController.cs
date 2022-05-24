using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.API.Attributes;
using RecipeHub.API.DTO;
using RecipeHub.Domain.Abstractions;
using RecipeHub.Domain.Model;
using RecipeHub.Domain.Model.Exceptions;
using RecipeHub.Domain.Services;
using RecipeHub.Infrastructure.DBO;
using RecipeHub.Infrastructure.Repositories;
using RecipeHub.Infrastructure.Repositories.Enums;

namespace RecipeHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;

        public ArticlesController(IArticleService articleService, IMapper mapper)
        {
            _articleService = articleService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_articleService.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                return Ok(_articleService.ReadArticle(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException)
            {
                return NotFound("Recipe not found");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [JwtUserAuthorization]
        [HttpPost]
        public IActionResult PostArticle(ArticleDto dto)
        {
            try
            {
                var article = _mapper.Map<Article>(dto);
                article.UserId = Guid.Parse((string)HttpContext.Items["id"] ?? string.Empty);
                _articleService.CreateArticle(article);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException)
            {
                return NotFound("Article not found");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
            return Ok("Successfully created new article");
        }

        [JwtUserAuthorization]
        [HttpPut]
        public IActionResult UpdateArticle(UpdateArticleDto dto)
        {
            try
            {
                var article = _mapper.Map<Article>(dto);
                article.UserId = article.UserId = Guid.Parse((string)HttpContext.Items["id"] ?? string.Empty);
                _articleService.UpdateArticle(article);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException)
            {
                return NotFound("Article not found");
            }
            catch (InvalidUserIdException)
            {
                return Unauthorized("Invalid userId");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
            return Ok("Successfully updated article");
        }

        [JwtAdminAuthorization]
        [HttpDelete]
        public IActionResult DeleteArtcile(DeleteArticleDto dto)
        {
            var userId = Guid.Parse((string)HttpContext.Items["id"] ?? string.Empty);
            bool isAdmin = HttpContext.Items["isAdmin"].Equals("True");
            Article art;
            try
            {
                art = _articleService.ReadArticle(dto.ArticleId);
            }
            catch (InvalidOperationException)
            {
                return NotFound("Article not found!");
            }
            catch (Exception)
            {
                return Problem("Ooops, something went wrong, try again later!");
            }
            if (!isAdmin && userId != art.UserId) return Unauthorized();
            _articleService.DeleteArticle(dto.ArticleId);
            return Ok();
        }
    }
}
