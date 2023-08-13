using Azure.Core;
using BookstoreApi.Controllers;
using BookstoreApi.Repositories.Interface;
using BookstoreApi.Services.Interfaces;
using BookstoreApi.ViewModels;

namespace BookstoreApi.Services
{
    public class BooksService : IBooksService
    {
        private readonly ILogger<BooksService> _logger;
        private readonly IBookRepositories _bookRepositories;

        public BooksService(IBookRepositories bookRepositories, ILogger<BooksService> logger)
        {
            _bookRepositories = bookRepositories;
            _logger = logger;
        }

        public async Task<List<GetUpdateBooksModel>> GetBooks()
        {
            try
            {
                return await _bookRepositories.GetBooks();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during GetBooks");
                return null;
            }
        }

        public async Task<Response<int>> AddBooks(AddBooksModel request)
        {
            try
            {
                return await _bookRepositories.AddBooks(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during AddBooks");
                return new Response<int>()
                {
                    IsSuccessful = false,
                    Value = 0,
                    Message = "There was an error during AddBooks"
                };
            }
        }

        public async Task<Response<int>> UpdateBook(GetUpdateBooksModel request)
        {
            try
            {
                return await _bookRepositories.UpdateBook(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during UpdateBook");
                return new Response<int>()
                {
                    IsSuccessful = false,
                    Value = 0,
                    Message = "There was an error during UpdateBook"
                };
            }
        }

        public async Task<Response<int>> DeleteBook(int id)
        {
            try
            {
                return await _bookRepositories.DeleteBook(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error during DeleteBook");
                return new Response<int>()
                {
                    IsSuccessful = false,
                    Value = 0,
                    Message = "There was an error during DeleteBook"
                };
            }
        }
    }
}