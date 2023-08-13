using BookstoreSdk.ViewModels;

namespace BookstoreSdk.Clients.Interface
{
    public interface IUserClient
    {
        Task<SdkResponse<int>> RegisterUser(SdkRegisterUserModel request);
        Task<SdkResponse<string>> Login(SdkLoginRequestModel request);
    }
}