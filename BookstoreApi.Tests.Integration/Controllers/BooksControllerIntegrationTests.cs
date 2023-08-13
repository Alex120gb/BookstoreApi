using BookstoreApi.Common;
using BookstoreApi.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BookstoreApi.Tests.Integration.Controllers
{
    [TestFixture]
    public class BooksControllerIntegrationTests
    {
        private HttpClient _client;
        private MockJwtTokenGenerator _jwtTokenGenerator;
        private string _validJwtToken;

        [OneTimeSetUp]
        public void Setup()
        {
            var appFactory = new WebApplicationFactory<Startup>();

            _client = appFactory.CreateClient();

            _jwtTokenGenerator = new MockJwtTokenGenerator();
            _validJwtToken = _jwtTokenGenerator.GenerateJwtToken("TestUser");
        }

        [Test]
        public async Task Books_ReturnsSuccessStatusCode()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _validJwtToken);

            // Act
            var response = await _client.GetAsync("/BooksApi/GetAllBooks");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var books = await response.Content.ReadFromJsonAsync<List<GetUpdateBooksModel>>();
            Assert.NotNull(books);
        }

        [Test]
        public async Task Books_ReturnsFailUnauthorized()
        {
            // Arrange & Act
            var response = await _client.GetAsync("/BooksApi/GetAllBooks");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task AddBooks_ReturnsCreatedStatusCode()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _validJwtToken);
            // Remember to change
            // the title for every new test run
            var newBook = new AddBooksModel() 
            { 
                Title = "TestBook2-add-new-book", 
                Author = "IntegrationTest-Add-2",
                PublicationYear = "2023",
                Isbn = "TEST-2023-integration"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/BooksApi/AddBook", newBook);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var createdBookResponse = await response.Content.ReadFromJsonAsync<Response<int>>();
            Assert.NotNull(createdBookResponse);
            Assert.IsTrue(createdBookResponse.IsSuccessful == true);
        }

        [Test]
        public async Task AddBooks_ReturnsFailUnauthorized()
        {
            // Arrange
            var newBook = new AddBooksModel();

            // Act
            var response = await _client.PostAsJsonAsync("/BooksApi/AddBook", newBook);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task UpdateBook_ReturnsUpdatedStatusCode()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _validJwtToken);
            var updateBook = new GetUpdateBooksModel()
            {
                Id = 54,
                Title = "TestBook - Testing Update Functionality",
                Author = "IntegrationTest-2",
                PublicationYear = "2023",
                Isbn = "TEST-2023-integration"
            };

            // Act
            var response = await _client.PutAsJsonAsync("/BooksApi/UpdateExistingBook", updateBook);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var updatedBookResponse = await response.Content.ReadFromJsonAsync<Response<int>>();
            Assert.NotNull(updatedBookResponse);
            Assert.IsTrue(updatedBookResponse.IsSuccessful == true);
        }

        [Test]
        public async Task UpdateBook_ReturnsFailUnauthorized()
        {
            // Arrange
            var updateBook = new GetUpdateBooksModel();

            // Act
            var response = await _client.PutAsJsonAsync("/BooksApi/UpdateExistingBook", updateBook);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task DeleteBook_ReturnsDeletedStatusCode()
        {
            // Arrange
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _validJwtToken);
            var deleteBook = 55;

            // Act
            var response = await _client.DeleteAsync("/BooksApi/DeleteExistingBook/" + deleteBook);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var deletedBookResponse = await response.Content.ReadFromJsonAsync<Response<int>>();
            Assert.NotNull(deletedBookResponse);
            Assert.IsTrue(deletedBookResponse.IsSuccessful == true);
        }

        [Test]
        public async Task DeleteBook_ReturnsFailUnauthorized()
        {
            // Arrange
            var deleteBook = 55;

            // Act
            var response = await _client.DeleteAsync("/BooksApi/DeleteExistingBook/" + deleteBook);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Unauthorized));
        }
    }
}
