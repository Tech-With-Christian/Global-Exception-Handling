using GlobalExceptionHandling.Entities;
using GlobalExceptionHandling.Exceptions;
using GlobalExceptionHandling.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalExceptionHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAll() 
        {
            var books = await _bookService.GetBooksAsync();
            return Ok(books);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetBookAsync(id);
            if (book == null)
            {
                throw new NotFoundException($"A product from the database with ID: {id} could not be found.");
            }
            return Ok(book);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBook(Book book)
        {
            var addResult = await _bookService.AddBookAsync(book);
            return Ok(addResult);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateBook(Book book)
        {
            var updateResult = await _bookService.UpdateBookAsync(book);
            return Ok(updateResult);
        }

        [HttpDelete("delete/{id}")]
        public async Task<bool> DeleteBook(int id)
        {
            return await _bookService.DeleteBookAsync(id);
        }
    }
}
