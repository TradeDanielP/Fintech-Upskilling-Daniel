using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FintechLibrary.DTOs;

namespace FintechLibrary.Services
{
    public class ApiCallerService
    {
        private readonly HttpClient _httpClient;

        public ApiCallerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<T>> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<T>>(json);
        }

        public async Task<T> PostAsync<T>(string endpoint, T data)
        {
            var jsonContent = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error posting data: {response.ReasonPhrase}");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task<List<AccountDTO>> GetAllCuentasAsync()
        {
            return await GetAsync<AccountDTO>("api/accounts");
        }

        public async Task<TransactionDTO> RegistrarTransaccionAsync(TransactionDTO transacion)
        {
            return await PostAsync<TransactionDTO>("api/transactions", transacion);
        }
    }
}
