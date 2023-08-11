using BookstoreSdk.ViewModels;

namespace BookstoreSdk.Services.Interfaces
{
    public interface IClientUserService
    {
        Task<SdkResponse<int>> RegisterUser(SdkRegisterUserModel request);
        Task<SdkResponse<string>> Login(SdkLoginRequestModel request);
    }
}
