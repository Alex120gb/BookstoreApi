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

        public bool UsernameExists(string username)
        {
            return Users.Any(p => p.Username == username);
        }
    }
}