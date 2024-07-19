using Graduation_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionsController : ControllerBase
    {

        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _dbContext;

        public PredictionsController(HttpClient httpClient, ApplicationDbContext dbContext)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Predict([FromBody] PredictionRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/predict", request);
            if (response.IsSuccessStatusCode)
            {
                var prediction = await response.Content.ReadFromJsonAsync<PredictionResponse>();


                var record = new PredictionRecord
                {
                    Features = JsonSerializer.Serialize(request.Features),
                    Prediction = prediction.Prediction,
                    CreatedAt = DateTime.UtcNow
                };

                _dbContext.PredictionRecords.Add(record);
                await _dbContext.SaveChangesAsync();

                return Ok(prediction);
            }
            return StatusCode((int)response.StatusCode);
        }

    }
}
