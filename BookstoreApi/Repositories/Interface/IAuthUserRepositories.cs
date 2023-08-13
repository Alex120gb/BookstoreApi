namespace BookstoreApi.Repositories.Interface
{
    public interface IAuthUserRepositories
    {
        Task<bool> CorrectUser(string username, string password);
    }
}
