using Graduation_Project.Dtos;
using System.Text;
using System.Text.Json;
using System.Net.Http;

namespace Graduation_Project.Services
{
    public class HttpServiceCollectionExtensions
    {
        private readonly HttpClient _httpClient;

        public HttpServiceCollectionExtensions(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpServiceCollectionExtensions()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(5) // زيادة المهلة إلى 5 دقائق
            };
        }

        public async Task<PredictionResponseDto> GetPrediction(PredictionRequestDto request)
        {
            var json = JsonSerializer.Serialize(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://192.168.2.106:5000/predict", data);
            var result = await response.Content.ReadAsStringAsync();

            var predictionResponse = JsonSerializer.Deserialize<PredictionResponseDto>(result);

            return predictionResponse;
        }

    }
}
