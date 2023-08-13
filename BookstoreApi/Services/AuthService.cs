using BookstoreApi.Repositories.Interface;
using BookstoreApi.Services.Interfaces;

namespace BookstoreApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepositories _userRepositories;

        public AuthService(IConfiguration configuration, IUserRepositories userRepositories)
        {
            _configuration = configuration;
            _userRepositories = userRepositories;
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            return await _userRepositories.CorrectUser(username, password);
        }
    }
}