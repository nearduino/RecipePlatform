using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.InfrasctructureInterfaces;
using RecipeHub.Domain.Model;
using RecipeHub.Domain.Model.Enums;
using RecipeHub.Domain.Model.Exceptions;

namespace RecipeHub.Domain.Services.Implementation
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeInfrastructureService _recipeInfrastructureService;
        private readonly IIngredientInfrastructureService _ingredientInfrastructureService;

        public RecipeService(IRecipeInfrastructureService recipeInfrastructureService, IIngredientInfrastructureService ingredientInfrastructureService)
        {
            _recipeInfrastructureService = recipeInfrastructureService;
            _ingredientInfrastructureService = ingredientInfrastructureService;
        }

        public IEnumerable<Recipe> GetAll()
        {
            return _recipeInfrastructureService.GetAll().ToList();
        }

        public void CreateNewRecipe(Category category, string name, string description, string instructions, uint preparationTime,
            IEnumerable<Tuple<int, int>> recipeIngredientIds, int userId)
        {
            var ingrIds = recipeIngredientIds as Tuple<int, int>[] ?? recipeIngredientIds.ToArray();
            var ingredients = ExtractIngredients(ingrIds);
            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
            foreach (var recipeIngredient in ingredients.Zip(ingrIds))
            {
                recipeIngredients.Add(new RecipeIngredient(recipeIngredient.Second.Item2, recipeIngredient.First));
            }
            Recipe recipe = new Recipe(category, name, description, instructions, preparationTime, recipeIngredients,
                userId);
            _recipeInfrastructureService.SaveRecipe(recipe);
        }

        public Recipe GetById(int id)
        {
            return _recipeInfrastructureService.GetById(id);
        }

        private IEnumerable<Ingredient> ExtractIngredients(Tuple<int, int>[] ingrIds)
        {
            List<int> ingredientIds = new List<int>();
            foreach (var ingrId in ingrIds) ingredientIds.Add(ingrId.Item1);
            var ingredients = _ingredientInfrastructureService.GetIngredientsByIds(ingredientIds);
            if (ingredients.Count() != ingrIds.Count()) throw new EntityNotFoundException("ingredient");
            return ingredients;
        }
    }
}
