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
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User=sa;Password={dbPassword};TrustServerCertificate=true;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        public bool UsernameExists(string username)
        {
            return Users.Any(p => p.Username == username);
        }
    }
}
