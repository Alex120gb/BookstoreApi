using BookstoreSdk.Clients.Interface;
using BookstoreSdk.ViewModels;
using BookstoreSdk.Services.Interfaces;

namespace BookstoreSdk.Clients
{
    public class UserClient : IUserClient
    {
        private readonly IClientUserService _clientUserService;

        public UserClient(IClientUserService clientUserService)
        {
            _clientUserService = clientUserService;
        }

        public async Task<SdkResponse<int>> RegisterUser(SdkRegisterUserModel request)
        {
            return await _clientUserService.RegisterUser(request);
        }

        public async Task<SdkResponse<string>> Login(SdkLoginRequestModel request)
        {
            return await _clientUserService.Login(request);
        }
    }
}
