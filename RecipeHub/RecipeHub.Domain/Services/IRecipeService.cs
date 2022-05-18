using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.InfrasctructureInterfaces;
using RecipeHub.Domain.Model;
using RecipeHub.Domain.Model.Enums;

namespace RecipeHub.Domain.Services
{
    public interface IRecipeService
    {
        public IEnumerable<Recipe> GetAll();

        public void CreateNewRecipe(Category category, string name, string description, string instructions,
            uint preparationTime, IEnumerable<Tuple<Guid, int>> recipeIngredientIds, Guid userId);
        public Recipe GetById(Guid id);

        public void UpdateRecipe(Guid id, Category category, string name, string description, string instructions,
            uint preparationTime, IEnumerable<Tuple<Guid, int>> recipeIngredientIds, Guid userId);
        public void DeleteRecipe(Guid id);
    }
}
