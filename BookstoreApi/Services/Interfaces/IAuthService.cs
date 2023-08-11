namespace BookstoreApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> AuthenticateUser(string username, string password);
    }
}
