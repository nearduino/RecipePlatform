using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Aggregator.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecipeHub.API.DTO;

namespace Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : BaseAggregatorController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _httpClient.GetAsync(_recipeHubBaseUrl + "Articles");
            var returnValue = await result.Content.ReadAsStringAsync();
            switch (result.StatusCode)
            {
                case HttpStatusCode.NotFound: return NotFound(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: return Problem(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: return BadRequest(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.Unauthorized: return Unauthorized(await result.Content.ReadAsStringAsync());
                default: return Ok(JsonConvert.DeserializeObject<List<ArticleDto>>(returnValue));
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _httpClient.GetAsync(_recipeHubBaseUrl + "Articles/" + id);
            switch (result.StatusCode)
            {
                case HttpStatusCode.NotFound: return NotFound(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: return Problem(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: return BadRequest(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.Unauthorized: return Unauthorized(await result.Content.ReadAsStringAsync());
                default: return Ok(JsonConvert.DeserializeObject<ArticleDto>(await result.Content.ReadAsStringAsync()));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostArticle(NewArticleDto dto)
        {
            LogIn();
            var result = await _httpClient.PostAsync(_recipeHubBaseUrl + "Articles", GetContent(dto));
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
        public async Task<IActionResult> PutArticle(UpdateArticleDto dto)
        {
            LogIn();
            var result = await _httpClient.PutAsync(_recipeHubBaseUrl + "Articles", GetContent(dto));
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
        public async Task<IActionResult> DeleteArticle(DeleteArticleDto dto)
        {
            LogIn();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_recipeHubBaseUrl + "Articles") ,
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
