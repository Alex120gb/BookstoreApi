using BookstoreSdk.Clients.Interface;
using BookstoreSdk.Services.Interfaces;
using BookstoreSdk.ViewModels;

namespace BookstoreSdk.Clients
{
    public class BookClient : IBookClient
    {
        private readonly IClientBookService _clientBookService;

        public BookClient(IClientBookService clientBookService)
        {
            _clientBookService = clientBookService;
        }

        public async Task<SdkResponse<List<SdkGetUpdateBooksModel>>> GetBooks(string bearerToken)
        {
            return await _clientBookService.GetBooks(bearerToken);
        }

        public async Task<SdkResponse<int>> AddBooks(SdkBooksModel request, string bearerToken)
        {
            return await _clientBookService.AddBooks(request, bearerToken);
        }

        public async Task<SdkResponse<int>> UpdateBook(SdkGetUpdateBooksModel request, string bearerToken)
        {
            return await _clientBookService.UpdateBook(request, bearerToken);
        }

        public async Task<SdkResponse<int>> DeleteBook(int id, string bearerToken)
        {
            return await _clientBookService.DeleteBook(id, bearerToken);
        }
    }
}