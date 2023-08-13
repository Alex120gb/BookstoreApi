using BookstoreSdk.Clients.Interface;
using BookstoreSdk.Clients;
using BookstoreSdk.Services;
using BookstoreSdk.ViewModels;

namespace BookstoreApi.Tests.Integration.SdkClients
{
    public class UsersSdkClientIntegrationTests
    {
        private IUserClient _userClient;

        [OneTimeSetUp]
        public void Setup()
        {
            _userClient = new UserClient(new ClientUserService("http://localhost:8080"));
        }

        [Test]
        public async Task Register_Success()
        {
            // Arrange
            var newUser = new SdkRegisterUserModel() 
            { 
                Username = "Test_Client",
                Email = "integration@Test",
                Password = "asdf123"
            };

            // Act
            var response = await _userClient.RegisterUser(newUser);

            // Assert
            Assert.IsTrue(response.IsSuccessful == true);
        }

        [Test]
        public async Task Register_Fail_UserExists()
        {
            // Arrange
            var newUser = new SdkRegisterUserModel()
            {
                Username = "Test_Client",
                Email = "integration@Test",
                Password = "asdf123"
            };

            // Act
            var response = await _userClient.RegisterUser(newUser);

            // Assert
            Assert.IsTrue(response.IsSuccessful == false);
        }

        [Test]
        public async Task Login_Success()
        {
            // Arrange
            var loginUser = new SdkLoginRequestModel()
            {
                Username = "Test_Client",
                Password = "asdf123"
            };

            // Act
            var response = await _userClient.Login(loginUser);

            // Assert
            Assert.IsTrue(response.IsSuccessful == true);
        }

        [Test]
        public async Task Login_Fail_IncorrectCredentials()
        {
            // Arrange
            var loginUser = new SdkLoginRequestModel()
            {
                Username = "WrongName",
                Password = "55555555555555"
            };

            // Act
            var response = await _userClient.Login(loginUser);

            // Assert
            Assert.IsTrue(response.IsSuccessful == false);
        }
    }
}
