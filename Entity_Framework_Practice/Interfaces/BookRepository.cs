using Entity_Framework_Practice.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Entity_Framework_Practice.Interfaces
{
    public class BookRepository : IBookRepository
    {
        private ApiContext _apiContext;
        public BookRepository(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }

        public async Task AddBook(Book book)
        {
            try
            {
                _apiContext.Books.Add(book);
                await _apiContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error " + ex.ToString());
            }
        }

        public Task<bool> CheckIfBookExists(int id = 0)
        {
            bool exists = _apiContext.Books.Any(x => x.Id == id);

            return Task.FromResult(exists);
        }

        public async Task DeleteBook(int id)
        {
            try
            {
                var book = _apiContext.Books.FirstOrDefault(x => x.Id == id);

                if (book != null)
                {
                    _apiContext.Books.Remove(book);
                }

                await _apiContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error " + ex.ToString());
            }
        }

        public Task<List<Book>> GetAllBooks()
        {
            List<Book> books = _apiContext.Books.ToList();

            return Task.FromResult(books);
        }

        public Task<Book> GetBookById(int id)
        {
            Book book = new Book();
            try
            {
                book = _apiContext.Books.FirstOrDefault(x => x.Id == id);

                if (book != null)
                {
                    return Task.FromResult(book);
                }

                return Task.FromResult(book);
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error " + ex.ToString());
                return Task.FromResult(book);
            }
        }
    }
}
