using AutoMapper;
using BookstoreApi.Common;
using BookstoreApi.Repositories.Interface;
using BookstoreApi.Services.Interfaces;
using BookstoreApi.TableDbContext;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.Repositories
{
    public class AuthUserRepositories : IAuthUserRepositories
    {
        private readonly UserDbContext _context;

        public AuthUserRepositories(UserDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CorrectUser(string username, string password)
        {
            string hashedPassword = PasswordHasher.HashPassword(password);

            return await _context.Users.AnyAsync(u => u.Username == username && u.Password == hashedPassword);
        }
    }
}
