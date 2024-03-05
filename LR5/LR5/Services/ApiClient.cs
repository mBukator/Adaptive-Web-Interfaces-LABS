using Microsoft.AspNetCore.Mvc;

namespace LR5.Services {
    public class ApiClient {
        public readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient) {
            _httpClient = httpClient;
        }


        public async Task<string> GetNYTPopularAsync() {
            try {
                string apiUrl = "https://api.nytimes.com/svc/mostpopular/v2/viewed/1.json?api-key=88V8nTGlAo5b6GZeUrVHIv1s3d0fOncx";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode) {
                    return await response.Content.ReadAsStringAsync();
                } else {
                    return $"ACHTUNG!!! ERROR: {response.StatusCode}";
                }
            } catch (Exception ex) {
                return $"Exception Error: {ex.Message}";
            }
        }

        public async Task<string> PostNYTPopularAsync(string category, int period) {
            try {
                string apiUrl = $"https://api.nytimes.com/svc/mostpopular/v2/{category}/{period}.json?api-key=88V8nTGlAo5b6GZeUrVHIv1s3d0fOncx";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode) {
                    return await response.Content.ReadAsStringAsync();
                } else {
                    return $"ACHTUNG!!! ERROR: {response.StatusCode}";
                }
            } catch (Exception ex) {
                return $"Exception Error: {ex.Message}";
            }
        }

    }
}
