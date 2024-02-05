using Entity_Framework_Practice.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Practice
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) 
        {
            
        }

        public DbSet<Book> Books {  get; set; }
    }
}
