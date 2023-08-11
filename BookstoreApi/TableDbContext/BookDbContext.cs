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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=Bookstore;User=sa;Password=B@@k2toR3S3rVer;TrustServerCertificate=true;");
        }

        public bool BookExists(string title)
        {
            return Books.Any(p => p.Title == title);
        }
    }
}
