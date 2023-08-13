using BookstoreApi.Repositories.Interface;
using BookstoreApi.Services.Interfaces;

namespace BookstoreApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthUserRepositories _authUserRepositories;

        public AuthService(IConfiguration configuration, IAuthUserRepositories authUserRepositories)
        {
            _configuration = configuration;
            _authUserRepositories = authUserRepositories;
        }

        public async Task<bool> AuthenticateUser(string username, string password)
        {
            return await _authUserRepositories.CorrectUser(username, password);
        }
    }
}