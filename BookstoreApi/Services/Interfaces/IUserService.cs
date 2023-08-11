using BookstoreApi.ViewModels;

namespace BookstoreApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<Response<int>> RegisterUser(RegisterUserModel userModel);
        Task<Response<string>> Login(LoginRequestModel request);
    }
}
