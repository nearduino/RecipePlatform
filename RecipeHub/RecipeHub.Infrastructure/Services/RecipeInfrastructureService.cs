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
            var dbo = _mapper.Map<RecipeDbo>(recipe);
            foreach (var ingr in dbo.RecipeIngredientsDbo) ingr.IngredientDbo = null;
                _recipeRepository.Add(dbo);
        }

        public Recipe GetById(Guid id)
        {
            var dbo = _recipeRepository.GetById(id, FetchType.Eager);
            var recipe = _mapper.Map<Recipe>(dbo);
            return recipe;
        }

        public void UpdateRecipe(Recipe recipe)
        {
            var fromDatabase = _recipeRepository.GetById(recipe.Id, FetchType.Eager);
            var dbo = _mapper.Map<RecipeDbo>(recipe);
            _mapper.Map(dbo, fromDatabase);
            foreach (var ingr in fromDatabase.RecipeIngredientsDbo) ingr.IngredientDbo = null;
            _recipeRepository.Update(fromDatabase);
        }

        public void Delete(Guid id)
        {
            _recipeRepository.Delete(_recipeRepository.GetById(id));
        }
    }
}
