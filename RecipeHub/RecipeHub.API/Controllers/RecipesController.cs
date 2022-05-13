using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeHub.Infrastructure.Repositories;
using RecipeHub.Infrastructure.Repositories.Enums;

namespace RecipeHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository _receiptRepo;

        public RecipesController(IRecipeRepository receiptRepo)
        {
            _receiptRepo = receiptRepo;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_receiptRepo.GetAll().ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_receiptRepo.GetById(id, FetchType.Eager));
        }
    }
}
