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
    public class ProfilesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public IActionResult CreateProfile([FromBody] CreateProfileDto request)
        {
            if (_context.Profiles.Any(p => p.Email == request.Email))
            {
                return Conflict("Email already exists.");
            }

            var profile = new Profile
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Gender = request.Gender,
                BabyWeight = request.BabyWeight,
                ChildLength = request.ChildLength,
                DateOfBirth = request.DateOfBirth
            };

            _context.Profiles.Add(profile);
            _context.SaveChanges();
            return Ok("Profile created successfully.");
        }

        [HttpPost("edit/{id}")]
        public IActionResult EditProfile(int id, [FromBody] EditProfileDto request)
        {
            var profile = _context.Profiles.FirstOrDefault(p => p.Id == id);
            if (profile == null)
            {
                return NotFound("Profile not found.");
            }

            profile.Name = request.Name;
            profile.Email = request.Email;
            profile.Password = request.Password;
            profile.Gender = request.Gender;
            profile.BabyWeight = request.BabyWeight;
            profile.ChildLength = request.ChildLength;
            profile.DateOfBirth = request.DateOfBirth;

            _context.SaveChanges();
            return Ok("Profile updated successfully.");
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] LogoutDto request)
        {
            var profile = _context.Profiles.FirstOrDefault(p => p.Email == request.Email);
            if (profile == null)
            {
                return NotFound("Profile not found.");
            }

            return Ok("Logged out successfully.");
        }

    }
}
