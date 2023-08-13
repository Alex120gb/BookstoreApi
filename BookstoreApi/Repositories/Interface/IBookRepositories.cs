using BookstoreApi.ViewModels;

namespace BookstoreApi.Repositories.Interface
{
    public interface IBookRepositories
    {
        Task<List<GetUpdateBooksModel>> GetBooks();
        Task<Response<int>> AddBooks(AddBooksModel request);
        Task<Response<int>> UpdateBook(GetUpdateBooksModel request);
        Task<Response<int>> DeleteBook(int id);
    }
}