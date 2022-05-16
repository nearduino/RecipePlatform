using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Domain.InfrasctructureInterfaces;
using RecipeHub.Domain.Model;
using RecipeHub.Infrastructure.DBO;
using RecipeHub.Infrastructure.Repositories;
using RecipeHub.Infrastructure.Mappers;
using RecipeHub.Infrastructure.Repositories.Enums;

namespace RecipeHub.Infrastructure.Services
{
    public class RecipeInfrastructureService : IRecipeInfrastructureService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeInfrastructureService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> GetAll()
        {
            var recipeDbos = _recipeRepository.
                GetAll().
                Include(r => r.RecipeIngredientsDbo).
                ThenInclude(r => r.IngredientDbo).
                ToList();
            List<Recipe> recipes = new List<Recipe>();
            foreach (var recipe in recipeDbos)
            {
                recipes.Add(Mapper.Map(recipe));
            }

            return recipes;
        }

        public void SaveRecipe(Recipe recipe)
        {
            _recipeRepository.Add(Mapper.Map(recipe));
        }

        public Recipe GetById(int id)
        {
            return Mapper.Map(_recipeRepository.GetById(id, FetchType.Eager));
        }

    }
}
