using Graduation_Project.Dtos;
using Graduation_Project.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionssController : ControllerBase
    {
        private readonly Services.HttpServiceCollectionExtensions _predictionService;

        public PredictionssController(Services.HttpServiceCollectionExtensions predictionService)
        {
            _predictionService = predictionService;
        }

        [HttpPost("predict")]
        public async Task<IActionResult> Predict([FromBody] PredictionRequestDto request)
        {
            if (request == null || request.InputArray == null || request.InputArray.Length == 0)
            {
                return BadRequest("Invalid request data.");
            }

            var prediction = await _predictionService.GetPrediction(request);
            return Ok(prediction);
        }

    }
}
