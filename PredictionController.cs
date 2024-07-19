using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public PredictionController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> Predict([FromBody] PredictionRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("http://127.0.0.1:5000/predict", request);
            if (response.IsSuccessStatusCode)
            {
                var prediction = await response.Content.ReadFromJsonAsync<PredictionResponse>();
                return Ok(prediction);
            }
            return StatusCode((int)response.StatusCode);
        }
    }
}
