using BookstoreSdk.ViewModels;

namespace BookstoreSdk.Services.Interfaces
{
    public interface IClientBookService
    {
        Task<List<SdkGetUpdateBooksModel>> GetBooks(string bearerToken);
        Task<SdkResponse<int>> AddBooks(SdkBooksModel request, string bearerToken);
        Task<SdkResponse<int>> UpdateBook(SdkGetUpdateBooksModel request, string bearerToken);
        Task<SdkResponse<int>> DeleteBook(int id, string bearerToken);
    }
}
