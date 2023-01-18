using BookWeb.API.Interfaces;
using BookWeb.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        protected Response _response;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            try
            {
                _response.Result = await _bookService.GetAllAsync();
                _response.DisplayMessage = "Book List";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Book does not exist";
                return NotFound(_response);
            }
            _response.Result = book;
            _response.DisplayMessage = "Book Information";
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Add(Book book)
        {
            try
            {
                var newBook = await _bookService.AddAsync(book);
                _response.Result = newBook;
                return CreatedAtAction("Add", new { id = newBook.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error while saving the record";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(Book book)
        {
            try
            {
                var updatedBook = await _bookService.UpdateAsync(book);
                _response.Result = updatedBook;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error updating the registry";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingBook = await _bookService.GetByIdAsync(id);

                if (existingBook != null)
                {
                    _response.Result = await _bookService.DeleteAsync(id);
                    _response.DisplayMessage = "Book successfully removed";
                    return Ok(_response);
                }
                _response.IsSuccess = false;
                _response.DisplayMessage = "Book does not exist";
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
    }
}
