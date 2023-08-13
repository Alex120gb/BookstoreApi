using BookstoreApi.Services.Interfaces;
using BookstoreApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersApiController : ControllerBase
    {
        private readonly ILogger<UsersApiController> _logger;
        private readonly IUserService _userService;

        public UsersApiController(ILogger<UsersApiController> logger,
                                  IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("RegisterUser")]
        public async Task<Response<int>> RegisterUser(RegisterUserModel request)
        {
            _logger.LogInformation("RegisterUser method was called");

            return await _userService.RegisterUser(request);
        }

        [HttpPost("UserLogin")]
        public async Task<Response<string>> Login(LoginRequestModel request)
        {
            _logger.LogInformation("Login method was called");

            return await _userService.Login(request);
        }
    }
}