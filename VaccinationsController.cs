using Graduation_Project.Dtos;
using Graduation_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public VaccinationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public IActionResult CreateVaccinationRecord([FromBody] VaccinationDto request)
        {
            var vaccination = new vaccination
            {
                Name = request.Name,
                Date = request.Date,
                IsCompleted = false
            };

            _context.Vaccinations.Add(vaccination);
            _context.SaveChanges();

            return Ok("Vaccination record created successfully.");
        }


        [HttpPost("{vaccinationId}/complete")]
        public IActionResult CompleteVaccination(int vaccinationId)
        {
            var vaccination = _context.Vaccinations.FirstOrDefault(v => v.Id == vaccinationId);
            if (vaccination == null)
            {
                return NotFound("Vaccination record not found.");
            }

            vaccination.IsCompleted = true;
            _context.SaveChanges();

            return Ok("Vaccination record marked as completed.");
        }
    }
}
