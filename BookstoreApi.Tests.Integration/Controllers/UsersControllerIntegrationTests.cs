using BookstoreApi.Common;
using BookstoreApi.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApi.Tests.Integration.Controllers
{
    [TestFixture]
    public class UsersControllerIntegrationTests
    {
        private HttpClient _client;

        [OneTimeSetUp]
        public void Setup()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
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
            var response = await _client.PostAsJsonAsync("/UsersApi/RegisterUser", newUser);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var newUserResponse = await response.Content.ReadFromJsonAsync<Response<int>>();
            Assert.NotNull(newUserResponse);
            Assert.IsTrue(newUserResponse.IsSuccessful == true);
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
            var response = await _client.PostAsJsonAsync("/UsersApi/UserLogin", loginUser);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            var loginUserResponse = await response.Content.ReadFromJsonAsync<Response<string>>();
            Assert.NotNull(loginUserResponse);
            Assert.IsTrue(loginUserResponse.IsSuccessful == true);
        }
    }
}
