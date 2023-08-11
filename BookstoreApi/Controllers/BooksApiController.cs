using BookstoreApi.Services.Interfaces;
using BookstoreApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BooksApiController : ControllerBase
    {
        private readonly ILogger<BooksApiController> _logger;
        private readonly IBooksService _booksService;
        private readonly HttpClient _httpClient;

        public BooksApiController(ILogger<BooksApiController> logger,
                                  IBooksService booksService,
                                  IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _booksService = booksService;
            _httpClient = httpClientFactory.CreateClient("ApiHttpClient");
        }

        [HttpGet("GetAllBooks")]
        public async Task<List<GetUpdateBooksModel>> GetBooks()
        {
            return await _booksService.GetBooks();
        }

        [HttpPost("AddBook")]
        public async Task<Response<int>> AddBooks(AddBooksModel request)
        {
            return await _booksService.AddBooks(request);
        }

        [HttpPut("UpdateExistingBook")]
        public async Task<Response<int>> UpdateBook(GetUpdateBooksModel request)
        {
            return await _booksService.UpdateBook(request);
        }

        [HttpDelete("DeleteExistingBook/{id}")]
        public async Task<Response<int>> DeleteBook(int id)
        {
            return await _booksService.DeleteBook(id);
        }
    }
}