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
    public class CommentsController : BaseAggregatorController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _httpClient.GetAsync(_recipeHubBaseUrl + "Comments");
            var returnValue = await result.Content.ReadAsStringAsync();
            switch (result.StatusCode)
            {
                case HttpStatusCode.NotFound: return NotFound(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: return Problem(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: return BadRequest(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.Unauthorized: return Unauthorized(await result.Content.ReadAsStringAsync());
                default: return Ok(JsonConvert.DeserializeObject<List<CommentDto>>(returnValue));
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _httpClient.GetAsync(_recipeHubBaseUrl + "Comments/" + id);
            switch (result.StatusCode)
            {
                case HttpStatusCode.NotFound: return NotFound(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.InternalServerError: return Problem(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.BadRequest: return BadRequest(await result.Content.ReadAsStringAsync());
                case HttpStatusCode.Unauthorized: return Unauthorized(await result.Content.ReadAsStringAsync());
                default: return Ok(JsonConvert.DeserializeObject<CommentDto>(await result.Content.ReadAsStringAsync()));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(NewCommentDto dto)
        {
            LogIn();
            var result = await _httpClient.PostAsync(_recipeHubBaseUrl +  "Comments", GetContent(dto));
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
        public async Task<IActionResult> PutComment(UpdateCommentDto dto)
        {
            LogIn();
            var result = await _httpClient.PutAsync(_recipeHubBaseUrl + "Comments", GetContent(dto));
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
        public async Task<IActionResult> DeleteComment(DeleteCommentDto dto)
        {
            LogIn();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_recipeHubBaseUrl + "Comments"),
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
