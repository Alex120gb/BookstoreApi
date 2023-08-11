using BookstoreApi.ViewModels;

namespace BookstoreApi.Services.Interfaces
{
    public interface IBooksService
    {
        Task<List<GetUpdateBooksModel>> GetBooks();
        Task<Response<int>> AddBooks(AddBooksModel bookModel);
        Task<Response<int>> UpdateBook(GetUpdateBooksModel bookModel);
        Task<Response<int>> DeleteBook(int id);
    }
}
