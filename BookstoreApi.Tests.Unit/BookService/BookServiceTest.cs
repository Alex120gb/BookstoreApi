using BookstoreApi.Repositories.Interface;
using BookstoreApi.Services;
using BookstoreApi.ViewModels;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookstoreApi.Tests.Unit.BookService
{
    public class BookServiceTest
    {
        private Mock<IBookRepositories> _bookRepositories;
        private Mock<ILogger<BooksService>> _logger;
        private BooksService _sut;

        [SetUp]
        public void init()
        {
            _bookRepositories = new Mock<IBookRepositories>();
            _logger = new Mock<ILogger<BooksService>>();

            _sut = new BooksService(_bookRepositories.Object, _logger.Object);
        }

        [Test]
        public void GetBooks_Success()
        {
            // Arrange
            var books = new Response<List<GetUpdateBooksModel>>();
            _bookRepositories.Setup(x => x.GetBooks())
                .ReturnsAsync(books)
                .Verifiable();

            // Act
            var result = _sut.GetBooks().Result;

            // Assert
            Assert.NotNull(result);
            Mock.Verify();
        }

        [Test]
        public void GetBooks_Throw_Exception()
        {
            // Arrange
            _bookRepositories.Setup(x => x.GetBooks())
                .ThrowsAsync(new Exception());

            // Act
            var result = _sut.GetBooks().Result;

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void AddBooks_Success()
        {
            // Arrange
            var request = new AddBooksModel()
            {
                Title = "Test_Title",
                Author = "Test_Author",
                PublicationYear = "2023",
                Isbn = "ISBN_Number"
            };

            var response = new Response<int>() 
            { 
                IsSuccessful = true 
            };

            _bookRepositories.Setup(x => x.AddBooks(It.IsAny<AddBooksModel>()))
                .ReturnsAsync(response)
                .Verifiable();

            // Act
            var result = _sut.AddBooks(request).Result;

            // Assert
            Assert.IsTrue(result.IsSuccessful == true);
            Mock.Verify();
        }

        [Test]
        public void AddBooks_Faill_NullRequest()
        {
            // Arrange & Act
            var result = _sut.AddBooks(It.IsAny<AddBooksModel>()).Result;

            // Assert
            Assert.IsTrue(result.IsSuccessful == false);
            Mock.Verify();
        }

        [Test]
        public void AddBooks_Throw_Exception()
        {
            // Arrange
            _bookRepositories.Setup(x => x.AddBooks(It.IsAny<AddBooksModel>()))
                .ThrowsAsync(new Exception());

            // Act
            var result = _sut.AddBooks(It.IsAny<AddBooksModel>()).Result;

            // Assert
            Assert.IsTrue(result.IsSuccessful == false);
        }

        [Test]
        public void UpdateBook_Success()
        {
            // Arrange
            var request = new GetUpdateBooksModel() 
            {
                Title = "Test_Title",
                Author = "Test_Author",
                PublicationYear = "2023",
                Isbn = "ISBN_Number"
            };

            var response = new Response<int>() 
            { 
                IsSuccessful = true 
            };

            _bookRepositories.Setup(x => x.UpdateBook(It.IsAny<GetUpdateBooksModel>()))
                .ReturnsAsync(response)
                .Verifiable();

            // Act
            var result = _sut.UpdateBook(request).Result;

            // Assert
            Assert.IsTrue(result.IsSuccessful == true);
            Mock.Verify();
        }

        [Test]
        public void UpdateBook_Faill_NullRequest()
        {
            // Arrange & Act
            var result = _sut.UpdateBook(It.IsAny<GetUpdateBooksModel>()).Result;

            // Assert
            Assert.IsTrue(result.IsSuccessful == false);
            Mock.Verify();
        }

        [Test]
        public void UpdateBook_Throw_Exception()
        {
            // Arrange
            _bookRepositories.Setup(x => x.UpdateBook(It.IsAny<GetUpdateBooksModel>()))
                .ThrowsAsync(new Exception());

            // Act
            var result = _sut.UpdateBook(It.IsAny<GetUpdateBooksModel>()).Result;

            // Assert
            Assert.IsTrue(result.IsSuccessful == false);
        }
            
        [Test]
        public void DeleteBook_Success()
        {
            // Arrange
            var response = new Response<int>() { IsSuccessful = true };
            _bookRepositories.Setup(x => x.DeleteBook(It.IsAny<int>()))
                .ReturnsAsync(response)
                .Verifiable();

            // Act
            var result = _sut.DeleteBook(It.IsAny<int>()).Result;

            // Assert
            Assert.IsTrue(result.IsSuccessful == true);
            Mock.Verify();
        }

        [Test]
        public void DeleteBook_Throw_Exception()
        {
            // Arrange
            _bookRepositories.Setup(x => x.DeleteBook(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            // Act
            var result = _sut.DeleteBook(It.IsAny<int>()).Result;

            // Assert
            Assert.IsTrue(result.IsSuccessful == false);
        }
    }
}