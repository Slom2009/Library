using Microsoft.AspNetCore.Mvc;
using MongoExample.Models;
using MongoExample.Services;

namespace MongoExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController :ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public BookController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }


        [HttpGet]
        public async Task<List<Book>> Get()
        {
            return await _mongoDbService.GetBooksAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            await _mongoDbService.CreateBookAsync(book);
            return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Book book)
        {
            await _mongoDbService.UpdateBookAsync(id, book);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string Id)
        {
            await _mongoDbService.DeleteBookAsync(Id);
            return NoContent();
        }

        [HttpPut("author/{id}")]
        public async Task<IActionResult> AddAuthorToBook(string id,[FromBody]string authorId)
        {
            await _mongoDbService.AddAuthorToBook(id,authorId);
            return NoContent();
        }

    }
}
