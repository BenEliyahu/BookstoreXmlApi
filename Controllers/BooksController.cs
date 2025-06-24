using Microsoft.AspNetCore.Mvc;
using BookstoreXmlApi.Models;
using BookstoreXmlApi.Services;

namespace BookstoreXmlApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_bookService.GetAll());

        [HttpGet("{isbn}")]
        public IActionResult GetByIsbn(string isbn)
        {
            var book = _bookService.GetByIsbn(isbn);
            return book is null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Book book)
        {
            if (book is null || string.IsNullOrWhiteSpace(book.ISBN))
                return BadRequest("Book or ISBN is missing");

            _bookService.Add(book);
            return CreatedAtAction(nameof(GetByIsbn), new { isbn = book.ISBN }, book);
        }

        [HttpPut("{isbn}")]
        public IActionResult Update(string isbn, [FromBody] Book book)
        {
            return _bookService.Update(isbn, book) ? NoContent() : NotFound();
        }

        [HttpDelete("{isbn}")]
        public IActionResult Delete(string isbn)
        {
            return _bookService.Delete(isbn) ? NoContent() : NotFound();
        }

        [HttpGet("report")]
        public IActionResult Report()
        {
            var html = _bookService.GenerateHtmlReport();
            return Content(html, "text/html");
        }
    }
}