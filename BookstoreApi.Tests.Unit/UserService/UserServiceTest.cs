using BookstoreApi.Repositories.Interface;
using BookstoreApi.Services;
using BookstoreApi.ViewModels;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookstoreApi.Tests.Unit.UserService
{
    public class UserServiceTest
    {
        private Mock<IUserRepositories> _userRepositories;
        private Mock<ILogger<UsersService>> _logger;
        private UsersService _sut;

        [SetUp]
        public void init()
        {
            _userRepositories = new Mock<IUserRepositories>();
            _logger = new Mock<ILogger<UsersService>>();

            _sut = new UsersService(_userRepositories.Object, _logger.Object);
        }

        [Test]
        public void RegisterUser_Success()
        {
            // Arrange
            var request = new RegisterUserModel() 
            { 
                Username = "Test_Name",
                Email = "Random@Email",
                Password = "test"
            };

            var response = new Response<int>() 
            { 
                IsSuccessful = true 
            };

            _userRepositories.Setup(x => x.RegisterUser(It.IsAny<RegisterUserModel>()))
                .ReturnsAsync(response)
                .Verifiable();

            // Act
            var result = _sut.RegisterUser(request).Result;

            // Assert
            Assert.IsTrue(result.IsSuccessful == true);
            Mock.Verify();
        }

        [Test]
        public void RegisterUser_Fail_NullRequest()
        {
            // Arrange & Act
            var result = _sut.RegisterUser(It.IsAny<RegisterUserModel>()).Result;

            // Assert
            Assert.IsTrue(result.IsSuccessful == false);
            Mock.Verify();
        }

        [Test]
        public void RegisterUser_Throw_Exception()
        {
            // Arrange
            _userRepositories.Setup(x => x.RegisterUser(It.IsAny<RegisterUserModel>()))
                .ThrowsAsync(new Exception());

            // Act
            var result = _sut.RegisterUser(It.IsAny<RegisterUserModel>()).Result;

            // Assert
            Assert.IsTrue(result.IsSuccessful == false);
        }

        [Test]
        public void Login_Success()
        {
            // Arrange
            var request = new LoginRequestModel()
            {
                Username = "Test_Name",
                Password = "test"
            };  

            var response = new Response<string>() 
            { 
                IsSuccessful = true 
            };

            _userRepositories.Setup(x => x.Login(It.IsAny<LoginRequestModel>()))
                .ReturnsAsync(response)
                .Verifiable();

            // Act
            var result = _sut.Login(request).Result;

            // Assert
            Assert.IsTrue(result.IsSuccessful == true);
            Mock.Verify();
        }

        [Test]
        public void Login_Fail_NullRequest()
        {
            // Arrange & Act
            var result = _sut.Login(It.IsAny<LoginRequestModel>()).Result;

            // Assert
            Assert.IsTrue(result.IsSuccessful == false);
            Mock.Verify();
        }

        [Test]
        public void Login_Throw_Exception()
        {
            // Arrange
            _userRepositories.Setup(x => x.Login(It.IsAny<LoginRequestModel>()))
                .ThrowsAsync(new Exception());

            // Act
            var result = _sut.Login(It.IsAny<LoginRequestModel>()).Result;

            // Assert
            Assert.IsTrue(result.IsSuccessful == false);
        }
    }
}