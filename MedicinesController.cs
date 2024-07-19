using Graduation_Project.Dtos;
using Graduation_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation_Project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicinesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost("create")]
        public IActionResult CreateMedicineRecord([FromBody] MedicineDto request)
        {
            var medicine = new Medicine
            {
                Name = request.Name,
                Description = request.Description
                
            };

            _context.Medicines.Add(medicine);
            _context.SaveChanges();

            return Ok("Medicine record created successfully.");
        }


        [HttpGet("{medicineId}")]
        public IActionResult GetMedicineDetails(int medicineId)
        {
            var medicine = _context.Medicines.FirstOrDefault(m => m.Id == medicineId);
            if (medicine == null)
            {
                return NotFound("Medicine record not found.");
            }

            
            return Ok(medicine);
        }
    }
}
