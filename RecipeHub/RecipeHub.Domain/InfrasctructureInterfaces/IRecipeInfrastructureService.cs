using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.Model;

namespace RecipeHub.Domain.InfrasctructureInterfaces
{
    public interface IRecipeInfrastructureService
    {
        public IEnumerable<Recipe> GetAll();
        public void SaveRecipe(Recipe recipe);
        public Recipe GetById(Guid id);
        void UpdateRecipe(Recipe recipe);
        void Delete(Guid id);
    }
}
