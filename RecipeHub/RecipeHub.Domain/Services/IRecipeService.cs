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
            uint preparationTime, IEnumerable<Tuple<int, int>> recipeIngredientIds, int userId);
        public Recipe GetById(int id);

        public void UpdateRecipe(int id, Category category, string name, string description, string instructions,
            uint preparationTime, IEnumerable<Tuple<int, int>> recipeIngredientIds, int userId);
        public void DeleteRecipe(int id);
    }
}
