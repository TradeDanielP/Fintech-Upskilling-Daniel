using System.Text;
using System.Text.Json;
using FintechLibrary.DTOs;

namespace FintechLibrary.Services
{
    public class ApiCallerService
    {
        private readonly HttpClient _httpClient;

        // Constructor que recibe un HttpClient y lo asigna a una variable privada
        public ApiCallerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Método genérico para realizar una petición GET a un endpoint y deserializar la respuesta a una lista de objetos del tipo T
        public async Task<List<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                // Realiza una petición GET al endpoint proporcionado
                var response = await _httpClient.GetAsync(endpoint);

                // Verifica si la respuesta no fue exitosa y lanza una excepción si es el caso
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error fetching data: {response.ReasonPhrase}");
                }

                // Lee el contenido de la respuesta como una cadena de texto
                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                // Deserializa la cadena JSON a una lista de objetos del tipo T
                return JsonSerializer.Deserialize<List<T>>(json, options);
            }
            catch (Exception ex)
            {
                // Captura cualquier excepción, la imprime en la consola y la vuelve a lanzar
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // Método genérico para realizar una petición POST a un endpoint con datos serializados y deserializar la respuesta a un objeto del tipo T
        public async Task<T> PostAsync<T>(string endpoint, T data)
        {
            // Serializa el objeto data a una cadena JSON
            var jsonContent = JsonSerializer.Serialize(data);
            // Crea un objeto StringContent con el JSON serializado, codificación UTF-8 y tipo de contenido "application/json"
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Realiza una petición POST al endpoint proporcionado con el contenido serializado
            var response = await _httpClient.PostAsync(endpoint, content);

            // Verifica si la respuesta no fue exitosa y lanza una excepción si es el caso
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error posting data: {response.ReasonPhrase}");
            }

            // Lee el contenido de la respuesta como una cadena de texto
            var json = await response.Content.ReadAsStringAsync();
            // Deserializa la cadena JSON a un objeto del tipo T
            return JsonSerializer.Deserialize<T>(json);
        }

        // Método para obtener una lista de todos los AccountDTO llamando al método GetAsync con el endpoint específico
        public async Task<List<AccountDTO>> GetAllAccountsAsync()
        {
            return await GetAsync<AccountDTO>("api/accounts");
        }

        // Método para registrar una transacción llamando al método PostAsync con el endpoint específico y el objeto TransactionDTO
        public async Task<TransactionDTO> RegisterTransactionAsync(TransactionDTO transaction)
        {
            return await PostAsync<TransactionDTO>("api/transactions/create", transaction);
        }
    }
}