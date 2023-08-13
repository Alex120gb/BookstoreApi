using BookstoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.TableDbContext
{
    public class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }

        public bool BookExists(string title)
        {
            return Books.Any(p => p.Title == title);
        }
    }
}