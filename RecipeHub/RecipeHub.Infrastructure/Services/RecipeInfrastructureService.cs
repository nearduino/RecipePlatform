using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Domain.InfrasctructureInterfaces;
using RecipeHub.Domain.Model;
using RecipeHub.Infrastructure.DBO;
using RecipeHub.Infrastructure.Repositories;
using RecipeHub.Infrastructure.Repositories.Enums;
using Mapper = RecipeHub.Infrastructure.Mappers.Mapper;

namespace RecipeHub.Infrastructure.Services
{
    public class RecipeInfrastructureService : IRecipeInfrastructureService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public RecipeInfrastructureService(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
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
                recipes.Add(_mapper.Map<Recipe>(recipe));
            }

            return recipes;
        }

        public void SaveRecipe(Recipe recipe)
        {
            _recipeRepository.Add(_mapper.Map<RecipeDbo>(recipe));
        }

        public Recipe GetById(int id)
        {
            var dbo = _recipeRepository.GetById(id, FetchType.Eager);
            var recipe = _mapper.Map<Recipe>(dbo);
            return recipe;
        }

        public void UpdateRecipe(Recipe recipe)
        {
            var dbo = _recipeRepository.GetById(recipe.Id, FetchType.Eager);
            dbo.Overwrite(_mapper.Map<RecipeDbo>(recipe));
            _recipeRepository.Update(dbo);
        }

        public void Delete(int id)
        {
            _recipeRepository.Delete(_recipeRepository.GetById(id));
        }
    }
}
