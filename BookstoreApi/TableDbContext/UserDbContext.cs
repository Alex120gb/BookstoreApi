using BookstoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.TableDbContext
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=Bookstore;User=sa;Password=B@@k2toR3S3rVer;TrustServerCertificate=true;");
        }

        public bool UsernameExists(string username)
        {
            return Users.Any(p => p.Username == username);
        }
    }
}
