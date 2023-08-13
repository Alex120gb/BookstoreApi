using BookstoreApi.ViewModels;

namespace BookstoreApi.Repositories.Interface
{
    public interface IUserRepositories
    {
        Task<Response<int>> RegisterUser(RegisterUserModel request);
        Task<Response<string>> Login(LoginRequestModel request);
    }
}