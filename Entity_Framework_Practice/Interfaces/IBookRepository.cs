using Entity_Framework_Practice.Models;

namespace Entity_Framework_Practice.Interfaces
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetAllBooks();
        public Task<Book> GetBookById(int id);
        public Task AddBook(Book book);
        public Task DeleteBook(int id);
        public Task<bool> CheckIfBookExists(int id = 0);
    }
}
