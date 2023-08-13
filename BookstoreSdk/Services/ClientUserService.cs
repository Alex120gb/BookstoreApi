using BookstoreSdk.Services.Interfaces;
using BookstoreSdk.ViewModels;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace BookstoreSdk.Services
{
    public class ClientUserService : IClientUserService
    {
        private readonly HttpClient _httpClient;

        public ClientUserService(string apiUrl)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(apiUrl);
        }

        public async Task<SdkResponse<int>> RegisterUser(SdkRegisterUserModel request)
        {
            try
            {
                var json = new StringContent(System.Text.Json.JsonSerializer.Serialize(request, typeof(SdkRegisterUserModel), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("/UsersApi/RegisterUser", json);
                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                SdkResponse<int> result = JsonConvert.DeserializeObject<SdkResponse<int>>(content);

                return result;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }

        public async Task<SdkResponse<string>> Login(SdkLoginRequestModel request)
        {
            try
            {
                var json = new StringContent(System.Text.Json.JsonSerializer.Serialize(request, typeof(SdkLoginRequestModel), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("/UsersApi/UserLogin", json);
                response.EnsureSuccessStatusCode(); 

                string content = await response.Content.ReadAsStringAsync();
                SdkResponse<string> result = JsonConvert.DeserializeObject<SdkResponse<string>>(content);

                return result;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }
    }
}