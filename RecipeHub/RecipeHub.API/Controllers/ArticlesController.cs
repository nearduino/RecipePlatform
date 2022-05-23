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

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var id = HttpContext.Items["id"];
            try
            {
                return Ok(_articleService.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
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
                _articleService.CreateArticle(_mapper.Map<Article>(dto));
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
        public IActionResult UpdateArtcile(UpdateArticleDto dto)
        {
            try
            {
                _articleService.UpdateArticle(_mapper.Map<Article>(dto));
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
            return Ok("Successfully created new article");
        }

        [JwtAdminAuthorization]
        [HttpDelete]
        public IActionResult DeleteArtcile(DeleteArticleDto dto)
        {
            var userId = Guid.Parse((string)HttpContext.Items["id"] ?? string.Empty);
            bool isAdmin = HttpContext.Items["isAdmin"].Equals("True");
            var art = _articleService.ReadArticle(dto.ArticleId);
            if (!isAdmin && userId != art.UserId) return Unauthorized();
            _articleService.DeleteArticle(dto.ArticleId);
            return Ok();
        }
    }
}
