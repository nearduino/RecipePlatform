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
using RecipeHub.Domain.Model;
using RecipeHub.Domain.Model.Enums;
using RecipeHub.Domain.Model.Exceptions;
using RecipeHub.Domain.Services;
using RecipeHub.Infrastructure.DBO;
using RecipeHub.Infrastructure.Repositories;
using RecipeHub.Infrastructure.Repositories.Enums;

namespace RecipeHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IMapper _mapper;

        public RecipesController(IRecipeService recipeService, IMapper mapper)
        {
            _recipeService = recipeService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<RecipeDto>>(_recipeService.GetAll()));
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
                return Ok(_recipeService.GetById(id));
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
        public IActionResult PostRecipe(NewRecipeDto dto)
        {
            List<Tuple<Guid, int>>ingredientIds = new List<Tuple<Guid, int>>();
            var userId = Guid.Parse((string)HttpContext.Items["id"] ?? string.Empty);
            foreach (var ingr in dto.Ingredients)
            {
                ingredientIds.Add(new Tuple<Guid, int>(ingr.IngredientId, ingr.Quantity));
            }

            try
            {
                _recipeService.CreateNewRecipe(
                    dto.Category,
                    dto.Name,
                    dto.Description,
                    dto.Instructions,
                    dto.PreparationTime,
                    ingredientIds,
                    userId);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case InvalidNameException:
                        return BadRequest("Enter valid recipe name!");
                    case InvalidDescriptionException:
                        return BadRequest("Enter valid recipe name!");
                    case InvalidRecipeInstructionsException:
                        return BadRequest("Enter recipe instructions!");
                    case EmptyRecipeIngredientsException:
                        return BadRequest("Specify at least one recipe ingredient");
                    case InvalidUserIdException:
                        return Unauthorized("Invalid user id");
                    case EntityNotFoundException:
                        return NotFound(ex.Message);
                    default:
                        return Problem(ex.Message);
                }
            }
            return Ok("Successfully created new recipe");
        }
        [JwtUserAuthorization]
        [HttpPut]
        public IActionResult UpdateRecipe(UpdateRecipeDto dto)
        {
            List<Tuple<Guid, int>> ingredientIds = new List<Tuple<Guid, int>>();
            var fromDatabase = _recipeService.GetById(dto.Id);
            if (!Guid.Parse((string)HttpContext.Items["id"] ?? string.Empty).Equals(fromDatabase.UserId))
            {
                return Unauthorized("Cannot edit recipes of another user");
            }
            foreach (var ingr in dto.Ingredients)
            {
                ingredientIds.Add(new Tuple<Guid, int>(ingr.IngredientId, ingr.Quantity));
            }

            ingredientIds = ingredientIds.OrderBy(i => i.Item1).ToList();
            _recipeService.UpdateRecipe(dto.Id,
                dto.Category,
                dto.Name,
                dto.Description,
                dto.Instructions,
                dto.PreparationTime,
                ingredientIds,
                fromDatabase.UserId);
            return Ok("Recipe updated successfully");
        }

        [JwtAdminAuthorization]
        [HttpDelete]
        public IActionResult DeleteRecipe(DeleteRecipeDto dto)
        {
            var userId = Guid.Parse((string)HttpContext.Items["id"] ?? string.Empty);
            bool isAdmin = HttpContext.Items["isAdmin"].Equals("True");
            var rec = _recipeService.GetById(dto.RecipeId);
            if (!isAdmin && userId != rec.UserId) return Unauthorized();
            _recipeService.DeleteRecipe(dto.RecipeId);
            return Ok();
        }
    }
}
