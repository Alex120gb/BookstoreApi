using BookstoreSdk.Clients;
using BookstoreSdk.Clients.Interface;
using BookstoreSdk.Services;
using BookstoreSdk.ViewModels;

namespace BookstoreApi.Tests.Integration.SdkClients
{
    [TestFixture]
    public class BooksSdkClientIntegrationTest
    {
        private IBookClient _bookClient;
        private MockJwtTokenGenerator _jwtTokenGenerator;
        private string _validJwtToken;

        [OneTimeSetUp]
        public void Setup()
        {
            _jwtTokenGenerator = new MockJwtTokenGenerator();
            _validJwtToken = _jwtTokenGenerator.GenerateJwtToken("TestUser");

            _bookClient = new BookClient(new ClientBookService("http://localhost:8080"));
        }

        [Test]
        public async Task Books_Success()
        {
            // Arrange & Act
            var response = await _bookClient.GetBooks(_validJwtToken);

            // Assert
            Assert.NotNull(response);
            Assert.IsNotEmpty(response.Value);
        }

        [Test]
        public async Task AddBooks_Success()
        {
            //Arrange
            var newBook = new SdkBooksModel() 
            { 
                Title = "Client_Integration_Test",
                Author = "Integration_Test",
                PublicationYear = "2023",
                Isbn = "some-randome-ISBN"
            };

            // Act
            var response = await _bookClient.AddBooks(newBook, _validJwtToken);

            // Assert
            Assert.IsTrue(response.IsSuccessful == true);
        }

        [Test]
        public async Task AddBooks_Fail_BookExists()
        {
            //Arrange
            var newBook = new SdkBooksModel()
            {
                Title = "Client_Integration_Test",
                Author = "",
                PublicationYear = "",
                Isbn = ""
            };

            // Act
            var response = await _bookClient.AddBooks(newBook, _validJwtToken);

            // Assert
            Assert.IsTrue(response.IsSuccessful == false);
        }

        [Test]
        public async Task UpdateBook_Success()
        {
            //Arrange
            var updateBook = new SdkGetUpdateBooksModel()
            {
                Id = 55,
                Title = "Client_Integration_Test - test update",
                Author = "Integration_Test",
                PublicationYear = "2023",
                Isbn = "some-randome-ISBN"
            };

            // Act
            var response = await _bookClient.UpdateBook(updateBook, _validJwtToken);

            // Assert
            Assert.IsTrue(response.IsSuccessful == true);
        }

        [Test]
        public async Task UpdateBook_Fail_BookNoneExistand()
        {
            //Arrange
            var updateBook = new SdkGetUpdateBooksModel()
            {
                Id = 9999,
                Title = "Client_Integration_Test - test update",
                Author = "Integration_Test",
                PublicationYear = "2023",
                Isbn = "some-randome-ISBN"
            };

            // Act
            var response = await _bookClient.UpdateBook(updateBook, _validJwtToken);

            // Assert
            Assert.IsTrue(response.IsSuccessful == false);
        }

        [Test]
        public async Task Delete_Success()
        {
            //Arrange
            var deleteBookId = 55;

            // Act
            var response = await _bookClient.DeleteBook(deleteBookId, _validJwtToken);

            // Assert
            Assert.IsTrue(response.IsSuccessful == true);
        }

        [Test]
        public async Task Delete_Fail_BookNoneExistand()
        {
            //Arrange
            var deleteBookId = 9999;

            // Act
            var response = await _bookClient.DeleteBook(deleteBookId, _validJwtToken);

            // Assert
            Assert.IsTrue(response.IsSuccessful == false);
        }
    }
}