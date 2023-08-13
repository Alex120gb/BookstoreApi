using BookstoreApi.ViewModels;

namespace BookstoreApi.Repositories.Interface
{
    public interface IUserRepositories
    {
        Task<Response<int>> RegisterUser(RegisterUserModel request);
        Task<Response<string>> Login(LoginRequestModel request);
        Task<bool> CorrectUser(string username, string password);
    }
}