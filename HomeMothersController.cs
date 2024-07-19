using Graduation_Project.Dtos;
using Graduation_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeMothersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeMothersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateHomeMother([FromBody] CreateHomeMotherDto request)
        {
            var homeMother = new HomeMother
            {
                ChildName = request.ChildName,
                DateOfBirth = request.DateOfBirth,
                BloodType = request.BloodType,
                Gender = request.Gender,
                Weight = request.Weight,
                Height = request.Height
            };

            CalculateAge(homeMother);

            _context.HomeMothers.Add(homeMother);
            await _context.SaveChangesAsync();

            return Ok("HomeMother record created successfully.");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetHomeMother(int id)
        {
            var homeMother = await _context.HomeMothers.FindAsync(id);
            if (homeMother == null)
            {
                return NotFound("HomeMother record not found.");
            }

            CalculateAge(homeMother);

            return Ok(homeMother);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateHomeMother(int id, [FromBody] UpdateHomeMotherDto request)
        {
            var homeMother = await _context.HomeMothers.FindAsync(id);
            if (homeMother == null)
            {
                return NotFound("HomeMother record not found.");
            }

            homeMother.Weight = request.Weight;
            homeMother.Height = request.Height;

            CalculateAge(homeMother);

            await _context.SaveChangesAsync();

            return Ok("HomeMother record updated successfully.");
        }

        private void CalculateAge(HomeMother homeMother)
        {
            var currentDate = DateTime.UtcNow;
            var age = currentDate.Year - homeMother.DateOfBirth.Year;
            if (homeMother.DateOfBirth.Date > currentDate.AddYears(-age)) age--;
            homeMother.Age = age;
        }
    }
}
