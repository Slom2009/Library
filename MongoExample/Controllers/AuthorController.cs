using Microsoft.AspNetCore.Mvc;
using MongoExample.Models;
using MongoExample.Services;

namespace MongoExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public AuthorController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [HttpGet]
        public async Task<List<Author>> Get()
        {
            return await _mongoDbService.GetAuthorAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author author)
        {
            await _mongoDbService.CreateAuthorAsync(author);
            return CreatedAtAction(nameof(Get),new { id=author.Id},author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id,Author author)
        {
            await _mongoDbService.UpdateAuthorAsync(id, author);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string Id) 
        {
            await _mongoDbService.DeleteAuthorAsync(Id);  
            return NoContent();
        }
    }
}
