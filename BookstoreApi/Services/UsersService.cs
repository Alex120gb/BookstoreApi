using BookstoreApi.Repositories.Interface;
using BookstoreApi.Services.Interfaces;
using BookstoreApi.ViewModels;

namespace BookstoreApi.Services
{
    public class UsersService : IUserService
    {
        private readonly ILogger<UsersService> _logger;
        private readonly IUserRepositories _userRepositories;

        public UsersService(IUserRepositories userRepositories, ILogger<UsersService> logger)
        {
            _userRepositories = userRepositories;
            _logger = logger;
        }

        public async Task<Response<int>> RegisterUser(RegisterUserModel request)
        {
            try
            {
                return await _userRepositories.RegisterUser(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during RegisterUser");
                return new Response<int>()
                {
                    IsSuccessful = false,
                    Value = 0,
                    Message = "There was an error during RegisterUser"
                };
            }
        }

        public async Task<Response<string>> Login(LoginRequestModel request)
        {
            try
            {
                return await _userRepositories.Login(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during Login");
                return new Response<string>()
                {
                    IsSuccessful = false,
                    Value = "",
                    Message = "There was an error during Login"
                };
            }
        }
    }
}