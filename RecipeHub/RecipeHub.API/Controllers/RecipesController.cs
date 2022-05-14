using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.API.DTO;
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
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_recipeService.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_recipeService.GetById(id));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult PostRecipe(NewRecipeDto dto)
        {
            List<Tuple<int, int>>ingredientIds = new List<Tuple<int, int>>();
            foreach (var ingr in dto.Ingredients)
            {
                ingredientIds.Add(new Tuple<int, int>(ingr.IngredientId, ingr.Quantity));
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
                    dto.UserId);
                return Ok("Successfully created new recipe");
            }
            catch (InvalidNameException)
            {
                return BadRequest("Enter valid recipe name!");
            }
            catch (InvalidDescriptionException)
            {
                return BadRequest("Enter recipe description!");
            }
            catch (InvalidRecipeInstructionsException)
            {
                return BadRequest("Enter recipe instructions!");
            }
            catch (EmptyRecipeIngredientsException)
            {
                return BadRequest("Specify at least one recipe ingredient");
            }
            catch (InvalidUserIdException)
            {
                return Unauthorized("Invalid user id");
            }
            catch (EntityNotFoundException)
            {
                return NotFound("Ingredient not found!");
            }
        }
    }
}
