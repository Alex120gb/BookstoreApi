using BookstoreSdk.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using BookstoreSdk.Services.Interfaces;
using System.Text.Json;

namespace BookstoreSdk.Services
{
    public class ClientBookService : IClientBookService
    {
        private readonly HttpClient _httpClient;

        public ClientBookService(string apiUrl)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(apiUrl);
        }

        public async Task<List<SdkGetUpdateBooksModel>> GetBooks(string bearerToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                HttpResponseMessage response = await _httpClient.GetAsync("/BooksApi/GetAllBooks");
                response.EnsureSuccessStatusCode(); 

                string content = await response.Content.ReadAsStringAsync();
                List<SdkGetUpdateBooksModel> result = JsonConvert.DeserializeObject<List<SdkGetUpdateBooksModel>>(content);

                return result;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }

        public async Task<SdkResponse<int>> AddBooks(SdkBooksModel request, string bearerToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                var json = new StringContent(System.Text.Json.JsonSerializer.Serialize(request, typeof(SdkBooksModel), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync("/BooksApi/AddBook", json);
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

        public async Task<SdkResponse<int>> UpdateBook(SdkGetUpdateBooksModel request, string bearerToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                var json = new StringContent(System.Text.Json.JsonSerializer.Serialize(request, typeof(SdkGetUpdateBooksModel), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PutAsync("/BooksApi/UpdateExistingBook", json);
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

        public async Task<SdkResponse<int>> DeleteBook(int id, string bearerToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                HttpResponseMessage response = await _httpClient.DeleteAsync("/BooksApi/DeleteExistingBook/" + id);
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
    }
}