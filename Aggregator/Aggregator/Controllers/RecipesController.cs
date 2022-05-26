using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecipeHub.API.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : BaseAggregatorController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<RecipesController> _logger;

        public RecipesController(ILogger<RecipesController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public async Task<IActionResult> GetRecipes()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                LogIn();
                var ingredient = new RecipeIngredientDto
                {
                    IngredientId = Guid.Parse("175dabcf-8e55-47a9-84ac-8f832646bdc7"),
                    Quantity = 2
                };
                var recipe = new NewRecipeDto
                {
                    Name = "Recept",
                    Description = "opis recepta",
                    Instructions = "instrukcijeblabla",
                    Category = RecipeHub.Domain.Model.Enums.Category.Lunch,
                    PreparationTime = 30,
                    Ingredients = new List<RecipeIngredientDto> { ingredient }

                };
                using (var content = new StringContent(JsonConvert.SerializeObject(recipe), System.Text.Encoding.UTF8, "application/json"))
                {

                    HttpResponseMessage result = _httpClient.PostAsync("https://internship-recipes.azurewebsites.net/api/Recipes", content).Result;

                    string returnValue = result.Content.ReadAsStringAsync().Result;
                    return Ok(returnValue);



                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _httpClient.GetAsync(_recipeHubBaseUrl + "Recipes");
            var returnValue = await result.Content.ReadAsStringAsync();
            switch (result.StatusCode)
            {
                case HttpStatusCode.NotFound: return NotFound(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: return Problem(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: return BadRequest(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.Unauthorized: return Unauthorized(await result.Content.ReadAsStringAsync());
                default: return Ok(JsonConvert.DeserializeObject<List<NewRecipeDto>>(returnValue));
            }
        }
    }
}
