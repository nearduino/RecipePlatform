using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecipeHub.API.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Aggregator.DTO;

namespace Aggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : BaseAggregatorController
    {
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
                default: return Ok(JsonConvert.DeserializeObject<List<RecipeDto>>(returnValue));
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _httpClient.GetAsync(_recipeHubBaseUrl + "Recipes/" + id);
            switch (result.StatusCode)
            {
                case HttpStatusCode.NotFound: return NotFound(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: return Problem(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: return BadRequest(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.Unauthorized: return Unauthorized(await result.Content.ReadAsStringAsync());
                default: return Ok(JsonConvert.DeserializeObject<DetailedRecipeDto>(await result.Content.ReadAsStringAsync()));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostRecipe(NewRecipeDto dto)
        {
            LogIn();
            var result = await _httpClient.PostAsync(_recipeHubBaseUrl + "Recipes", GetContent(dto));
            switch (result.StatusCode)
            {
                case HttpStatusCode.NotFound: return NotFound(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: return Problem(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: return BadRequest(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.Unauthorized: return Unauthorized(await result.Content.ReadAsStringAsync());
                default: return Ok(await result.Content.ReadAsStringAsync());
            }
        }
        [HttpPut]
        public async Task<IActionResult> PutRecipe(UpdateRecipeDto dto)
        {
            LogIn();
            var result = await _httpClient.PutAsync(_recipeHubBaseUrl + "Recipes", GetContent(dto));
            switch (result.StatusCode)
            {
                case HttpStatusCode.NotFound: return NotFound(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: return Problem(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: return BadRequest(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.Unauthorized: return Unauthorized(await result.Content.ReadAsStringAsync());
                default: return Ok(await result.Content.ReadAsStringAsync());
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteArticle(DeleteRecipeDto dto)
        {
            LogIn();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_recipeHubBaseUrl + "Recipes"),
                Content = GetContent(dto)
            };
            var result = await _httpClient.SendAsync(request);
            switch (result.StatusCode)
            {
                case HttpStatusCode.NotFound: return NotFound(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: return Problem(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: return BadRequest(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.Unauthorized: return Unauthorized(await result.Content.ReadAsStringAsync());
                default: return Ok(await result.Content.ReadAsStringAsync());
            }
        }
    }
}
