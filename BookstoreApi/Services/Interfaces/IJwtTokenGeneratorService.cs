namespace BookstoreApi.Services.Interfaces
{
    public interface IJwtTokenGeneratorService
    {
        string GenerateJwtTokentest(string username);
    }
}
