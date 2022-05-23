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
        private readonly ICommentInfrastructureService _commentInfrastructureService;

        public RecipeService(IRecipeInfrastructureService recipeInfrastructureService, IIngredientInfrastructureService ingredientInfrastructureService, ICommentInfrastructureService commentInfrastructureService)
        {
            _recipeInfrastructureService = recipeInfrastructureService;
            _ingredientInfrastructureService = ingredientInfrastructureService;
            _commentInfrastructureService = commentInfrastructureService;

        }

        public IEnumerable<Recipe> GetAll()
        {
            return _recipeInfrastructureService.GetAll().ToList();
        }

        public void CreateNewRecipe(Category category, string name, string description, string instructions, uint preparationTime,
            IEnumerable<Tuple<Guid, int>> recipeIngredientIds, Guid userId)
        {
            var ingrIds = recipeIngredientIds as Tuple<Guid, int>[] ?? recipeIngredientIds.ToArray();
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

        public Recipe GetById(Guid id)
        {
            return _recipeInfrastructureService.GetById(id);
        }

        public void UpdateRecipe(Guid id, Category category, string name, string description, string instructions, uint preparationTime,
            IEnumerable<Tuple<Guid, int>> recipeIngredientIds, Guid userId)
        {
            var ingrIds = recipeIngredientIds as Tuple<Guid, int>[] ?? recipeIngredientIds.ToArray();
            var ingredients = ExtractIngredients(ingrIds);
            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
            foreach (var recipeIngredient in ingredients.Zip(ingrIds))
            {
                recipeIngredients.Add(new RecipeIngredient(recipeIngredient.Second.Item2, recipeIngredient.First));
            }

            var recipe = _recipeInfrastructureService.GetById(id);
            recipe.UpdateDescription(description);
            recipe.UpdateIngredients(recipeIngredients);
            recipe.UpdateInstructions(instructions);
            recipe.UpdatePreparationTime(preparationTime);
            recipe.UpdateName(name);
            recipe.UpdateCategory(category);
            _recipeInfrastructureService.UpdateRecipe(recipe);
        }

        public void DeleteRecipe(Guid id)
        {
            _recipeInfrastructureService.Delete(id);
        }

        private IEnumerable<Ingredient> ExtractIngredients(Tuple<Guid, int>[] ingrIds)
        {
            List<Guid> ingredientIds = new List<Guid>();
            foreach (var ingrId in ingrIds) ingredientIds.Add(ingrId.Item1);
            var ingredients = _ingredientInfrastructureService.GetIngredientsByIds(ingredientIds);
            if (ingredients.Count() != ingrIds.Count()) throw new EntityNotFoundException("ingredient");
            ingredients = ingredients.OrderBy(i => i.Id);
            return ingredients;
        }

        private IEnumerable<Comment> GetComments(Guid recipeId)
        {
            return _commentInfrastructureService.GetCommentsByRecipeId(recipeId);
        }
    }
}
