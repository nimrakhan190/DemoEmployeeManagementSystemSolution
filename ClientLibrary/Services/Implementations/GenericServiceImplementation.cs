using System.Net.Http.Json;
using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;

namespace ClientLibrary.Services.Implementations
{
    public class GenericServiceImplementation<T> : IGenericServiceInterface<T>
    {
        private readonly GetHttpClient _getHttpClient;

        public GenericServiceImplementation(GetHttpClient getHttpClient)
        {
            _getHttpClient = getHttpClient;
        }

        // Create
        public async Task<GeneralResponse> Insert(T item, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PostAsJsonAsync($"{baseUrl}/add", item);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
                return result ?? new GeneralResponse(false, "Null response received.");
            }

            return new GeneralResponse(false, "Insert failed.");
        }

        // Read All
        public async Task<List<T>> GetAll(string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var results = await httpClient.GetFromJsonAsync<List<T>>($"{baseUrl}/all");
            return results ?? new List<T>();
        }

        // Read Single
        public async Task<T?> GetById(int id, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            return await httpClient.GetFromJsonAsync<T>($"{baseUrl}/single/{id}");
        }

        // Update
        public async Task<GeneralResponse> Update(T item, string baseUrl)
        {
            var httpClient = await _getHttpClient.GetPrivateHttpClient();
            var response = await httpClient.PutAsJsonAsync($"{baseUrl}/update", item);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
                return result ?? new GeneralResponse(false, "Null response received.");
            }

            return new GeneralResponse(false, "Update failed.");
        }

        // Delete
        // Delete
        public async Task<GeneralResponse> DeleteById(int id, string baseUrl)
        {
            try
            {
                if (id <= 0)
                    return new GeneralResponse(false, "Invalid ID provided.");

                var httpClient = await _getHttpClient.GetPrivateHttpClient();
                var response = await httpClient.DeleteAsync($"{baseUrl.TrimEnd('/')}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new GeneralResponse(false,
                        $"Delete failed. Status: {response.StatusCode}. Error: {errorContent}");
                }

                var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
                return result ?? new GeneralResponse(false, "Null response received.");
            }
            catch (HttpRequestException ex)
            {
                return new GeneralResponse(false, $"Network error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return new GeneralResponse(false, $"Unexpected error: {ex.Message}");
            }
        }
    }
}
