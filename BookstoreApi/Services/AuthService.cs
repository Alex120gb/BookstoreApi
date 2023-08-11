using BookstoreApi.Common;
using BookstoreApi.Services.Interfaces;
using BookstoreApi.TableDbContext;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(UserDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            string hashedPassword = PasswordHasher.HashPassword(password);

            return await _context.Users.AnyAsync(u => u.Username == username && u.Password == hashedPassword);
        }
    }
}
