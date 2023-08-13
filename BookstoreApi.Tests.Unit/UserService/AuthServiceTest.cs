using BookstoreApi.Repositories.Interface;
using BookstoreApi.Services;
using Moq;

namespace BookstoreApi.Tests.Unit.UserService
{
    public class AuthServiceTest
    {
        private Mock<IUserRepositories> _userRepositories;
        private Mock<Microsoft.Extensions.Configuration.IConfiguration> _configuration;
        private AuthService _sut;

        [SetUp]
        public void init()
        {
            _userRepositories = new Mock<IUserRepositories>();
            _configuration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();

            _sut = new AuthService(_configuration.Object, _userRepositories.Object);
        }

        [Test]
        public void AuthenticateUser_Success()
        {
            // Arrange
            var response = new Response<int>() { IsSuccessful = true };
            _userRepositories.Setup(x => x.CorrectUser(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true)
                .Verifiable();

            // Act
            var result = _sut.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>()).Result;

            // Assert
            Assert.IsTrue(result == true);
            Mock.Verify();
        }

        [Test]
        public void AuthenticateUser_Throw_Exception()
        {
            // Arrange
            _userRepositories.Setup(x => x.CorrectUser(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            // Act
            var result = _sut.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.ThrowsAsync<Exception>(() => result);
        }
    }
}