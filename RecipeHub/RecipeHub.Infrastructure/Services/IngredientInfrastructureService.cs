using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Domain.InfrasctructureInterfaces;
using RecipeHub.Domain.Model;
using RecipeHub.Infrastructure.Mappers;
using RecipeHub.Infrastructure.Repositories;

namespace RecipeHub.Infrastructure.Services
{
    public class IngredientInfrastructureService :IIngredientInfrastructureService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientInfrastructureService(IIngredientRepository repo)
        {
            _ingredientRepository = repo;
        }
        public IEnumerable<Ingredient> GetIngredientsByIds(IEnumerable<Guid> ids)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            var ingredintsDbos = _ingredientRepository.GetByIds(ids);
            foreach (var ingredientDbo in ingredintsDbos)
            {
                ingredients.Add(Mapper.Map(ingredientDbo));
            }

            return ingredients;
        }
    }
}
