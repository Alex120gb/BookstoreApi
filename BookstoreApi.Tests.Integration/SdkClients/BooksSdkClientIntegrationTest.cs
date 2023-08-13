using BookstoreApi.Common;
using BookstoreApi.ViewModels;
using BookstoreSdk.Clients;
using BookstoreSdk.Clients.Interface;
using BookstoreSdk.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApi.Tests.Integration.SdkClients
{
    [TestFixture]
    public class BooksSdkClientIntegrationTest
    {
        private HttpClient _httpClient;
        private ClientBookService _bookClientService;
        private IBookClient _bookClient;
        private MockJwtTokenGenerator _jwtTokenGenerator;
        private string _validJwtToken;

        [OneTimeSetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            //_httpClient.BaseAddress = new Uri("https://localhost:44388");

            _jwtTokenGenerator = new MockJwtTokenGenerator();
            _validJwtToken = _jwtTokenGenerator.GenerateJwtToken("TestUser");

            _bookClient = new BookClient(new ClientBookService("http://localhost:5000"));
        }

        [Test]
        public async Task Books_ReturnsSuccessStatusCode()
        {
            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _validJwtToken);

            // Act
            var response = await _bookClient.GetBooks(_validJwtToken);

            // Assert
            Assert.NotNull(response);
            Assert.IsNotEmpty(response);
        }

        [Test]
        public async Task Books_ReturnsFailUnauthorized()
        {
            // Arrange & Act
            var response = await _bookClientService.GetBooks(_validJwtToken);

            // Assert
            Assert.IsNull(response);
            Assert.IsEmpty(response);
        }
    }
}
