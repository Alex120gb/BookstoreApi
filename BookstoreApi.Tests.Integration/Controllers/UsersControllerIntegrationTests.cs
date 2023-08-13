using BookstoreApi.ViewModels;
using System.Net.Http.Json;

namespace BookstoreApi.Tests.Integration.Controllers
{
    [TestFixture]
    public class UsersControllerIntegrationTests
    {
        private HttpClient _httpClient;

        [OneTimeSetUp]
        public void Setup()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8080") // Update with your API's host and port
            };
        }

        [Test]
        public async Task RegisterUser_ReturnsSuccessStatusCode()
        {
            // Arrange
            var newUser = new RegisterUserModel()
            {
                Username = "IntegrationTest",
                Password = "password1234",
                Email = "some@email"
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/UsersApi/RegisterUser", newUser);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var newUserResponse = await response.Content.ReadFromJsonAsync<Response<int>>();
            Assert.NotNull(newUserResponse);
            Assert.IsTrue(newUserResponse.IsSuccessful == true);
        }

        [Test]
        public async Task RegisterUser_ReturnsFail_UserExists()
        {
            // Arrange
            var newUser = new RegisterUserModel()
            {
                Username = "IntegrationTest",
                Password = "password1234",
                Email = "some@email"
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/UsersApi/RegisterUser", newUser);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var newUserResponse = await response.Content.ReadFromJsonAsync<Response<int>>();
            Assert.NotNull(newUserResponse);
            Assert.IsTrue(newUserResponse.IsSuccessful == false);
        }

        [Test]
        public async Task Login_ReturnsSuccessStatusCode()
        {
            // Arrange
            var loginUser = new LoginRequestModel()
            {
                Username = "IntegrationTest",
                Password = "password1234"
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/UsersApi/UserLogin", loginUser);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var loginUserResponse = await response.Content.ReadFromJsonAsync<Response<string>>();
            Assert.NotNull(loginUserResponse);
            Assert.IsTrue(loginUserResponse.IsSuccessful == true);
        }

        [Test]
        public async Task Login_ReturnsFail_InvalidCreds()
        {
            // Arrange
            var loginUser = new LoginRequestModel()
            {
                Username = "IntegrationTest-wrong",
                Password = "666666666666666666"
            };

            // Act
            var response = await _httpClient.PostAsJsonAsync("/UsersApi/UserLogin", loginUser);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var loginUserResponse = await response.Content.ReadFromJsonAsync<Response<string>>();
            Assert.NotNull(loginUserResponse);
            Assert.IsTrue(loginUserResponse.IsSuccessful == false);
        }
    }
}