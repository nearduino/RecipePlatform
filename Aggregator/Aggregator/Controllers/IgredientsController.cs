using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : BaseAggregatorController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _httpClient.GetAsync(_recipeHubBaseUrl + "Ingredients");
            var returnValue = await result.Content.ReadAsStringAsync();
            switch (result.StatusCode)
            {
                case HttpStatusCode.NotFound: return NotFound(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: return Problem(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: return BadRequest(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.Unauthorized: return Unauthorized(await result.Content.ReadAsStringAsync());
                default: return Ok(JsonConvert.DeserializeObject<List<IngredientDto>>(returnValue));
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _httpClient.GetAsync(_recipeHubBaseUrl + "Ingredients/" + id);
            switch (result.StatusCode)
            {
                case HttpStatusCode.NotFound: return NotFound(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: return Problem(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: return BadRequest(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.Unauthorized: return Unauthorized(await result.Content.ReadAsStringAsync());
                default: return Ok(JsonConvert.DeserializeObject<IngredientDto>(await result.Content.ReadAsStringAsync()));
            }
        }
    }
}
