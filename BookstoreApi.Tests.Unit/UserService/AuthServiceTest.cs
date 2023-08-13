using BookstoreApi.Repositories.Interface;
using BookstoreApi.Services;
using Moq;

namespace BookstoreApi.Tests.Unit.UserService
{
    public class AuthServiceTest
    {
        private Mock<IAuthUserRepositories> _authUserRepositories;
        private Mock<Microsoft.Extensions.Configuration.IConfiguration> _configuration;
        private AuthService _sut;

        [SetUp]
        public void init()
        {
            _authUserRepositories = new Mock<IAuthUserRepositories>();
            _configuration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();

            _sut = new AuthService(_configuration.Object, _authUserRepositories.Object);
        }

        [Test]
        public void AuthenticateUser_Success()
        {
            // Arrange
            var response = new Response<int>() { IsSuccessful = true };
            _authUserRepositories.Setup(x => x.CorrectUser(It.IsAny<string>(), It.IsAny<string>()))
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
            _authUserRepositories.Setup(x => x.CorrectUser(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            // Act
            var result = _sut.AuthenticateUser(It.IsAny<string>(), It.IsAny<string>());

            // Assert
            Assert.ThrowsAsync<Exception>(() => result);
        }
    }
}