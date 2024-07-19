using Graduation_Project.Dtos;
using Graduation_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomepregnantsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomepregnantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateHome([FromBody] CreateHomeDto request)
        {
            var home = new Homepregnant
            {
                Name = request.Name,
                Email = request.Email,
                DateOfPregnancy = request.DateOfPregnancy
            };

            CalculateFetusDetails(home);

            _context.Homepregnants.Add(home);
            await _context.SaveChangesAsync();

            return Ok("Home record created successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHome(int id)
        {
            var home = await _context.Homepregnants.FindAsync(id);
            if (home == null)
            {
                return NotFound("Home record not found.");
            }

            CalculateFetusDetails(home);

            return Ok(home);
        }

        private void CalculateFetusDetails(Homepregnant home)
        {
            throw new NotImplementedException();
        }
    }
}
