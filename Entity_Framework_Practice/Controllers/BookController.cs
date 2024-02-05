using Entity_Framework_Practice.Interfaces;
using Entity_Framework_Practice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;

namespace Entity_Framework_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }


        [HttpGet("/GetAllBooks")]
        public ActionResult<List<Book>> GetAll()
        {
            return Ok(_bookRepository.GetAllBooks());
        }

        [HttpPost("/AddBook")]
        public async Task<IActionResult> AddBook(Book book)
        {
            try
            {
                await _bookRepository.AddBook(book);

                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }
        }

        [HttpHead("/CheckIfBookExists")]
        public ActionResult CheckIfBookExists(int id)
        {
            try
            {
                var result = _bookRepository.CheckIfBookExists(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("/DeleteBook")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookRepository.DeleteBook(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("/GetBookById")]
        public ActionResult<Book> GetBookById(int id)
        {
            try
            {
                var result = _bookRepository.GetBookById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
