using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.API.DTO;
using RecipeHub.Domain.Model;
using RecipeHub.Infrastructure.DBO;
using RecipeHub.Infrastructure.Repositories;
using RecipeHub.Infrastructure.Repositories.Enums;

namespace RecipeHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository _receiptRepo;
        private readonly IIngredientRepository _ingredientRepository;

        public RecipesController(IRecipeRepository receiptRepo, IIngredientRepository ingredientRepository)
        {
            _receiptRepo = receiptRepo;
            _ingredientRepository = ingredientRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_receiptRepo.GetAll().ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_receiptRepo.GetById(id, FetchType.Eager));
        }

        [HttpPut]
        public IActionResult PostRecipe(NewRecipeDto dto)
        {
            var ingredientIds = new List<int>();
            foreach(var ingredientDtos in dto.Ingredients)ingredientIds.Add(ingredientDtos.IngredientId);
            var ingredients = _ingredientRepository.GetByIds(ingredientIds);
            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
            foreach (var ingredient in ingredients.Zip(dto.Ingredients))
            {
                recipeIngredients.Add(new RecipeIngredient(ingredient.Second.Quantity, new Ingredient(ingredient.First.CaloriesPerUnit, ingredient.First.Name, ingredient.First.MeasureUnit, ingredient.First.Id)));
            }
            Recipe recipe = new Recipe(dto.Category, dto.Name, dto.Description, dto.Instructions, dto.PreparationTime,
                recipeIngredients, dto.UserId);
            var recipeDbo = new RecipeDbo
            {
                Category = recipe.Category,
                CommentsDbo = new List<CommentDbo>(),
                Description = recipe.Description,
                Name = recipe.Name,
                Instructions = recipe.Instructions,
                PreparationTime = recipe.PreparationTime,
                RecipeIngredientsDbo = new List<RecipeIngredientDbo>()
            };
            foreach (var ingredient in recipe.RecipeIngredients)
            {
                recipeDbo.RecipeIngredientsDbo.Add(new RecipeIngredientDbo
                {
                    IngredientDboId = ingredient.Ingredient.Id,
                    /*IngredientDbo = new IngredientDbo
                    {
                        CaloriesPerUnit = ingredient.Ingredient.CaloriesPerUnit,
                        Id = ingredient.Ingredient.Id,
                        MeasureUnit = ingredient.Ingredient.MeasureUnit,
                        Name = ingredient.Ingredient.Name
                    },*/
                    Quantity = ingredient.Quantity
                });
            }
            return Ok(_receiptRepo.Add(recipeDbo));
        }
    }
}
