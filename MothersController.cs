using Graduation_Project.Dtos;
using Graduation_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MothersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MothersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == request.Email && u.Password == request.Password);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok("Login successful.");
        }

        [HttpPost("signup")]
        public IActionResult Signup([FromBody] SignupRequestDto request)
        {
            if (_context.User.Any(u => u.Email == request.Email))
            {
                return Conflict("Email already exists.");
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Gender = request.Gender,
                BabyWeight = request.BabyWeight,
                ChildLength = request.ChildLength,
                DateOfBirth = request.DateOfBirth,
                Description = request.Description
            };

            _context.User.Add(user);
            _context.SaveChanges();
            return Ok("Signup successful.");
        }

        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordRequestDto request)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.OTP = GenerateOTP();
            user.OTPExpiration = DateTime.UtcNow.AddMinutes(5); 
            _context.SaveChanges();


            return Ok("OTP sent successfully.");
        }

        [HttpPost("resetpassword")]
        public IActionResult ResetPassword([FromBody] ForgotPasswordRequestDto request)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == request.Email && u.OTP == request.OTP && u.OTPExpiration > DateTime.UtcNow);
            if (user == null)
            {
                return Unauthorized("Invalid or expired OTP.");
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            user.Password = request.NewPassword;
            _context.SaveChanges();

            return Ok("Password reset successful.");
        }

        private string GenerateOTP()
        {
            Random rand = new Random();
            return rand.Next(10000, 99999).ToString(); 
        }


        [HttpPost("signup-pregnant")]
        public IActionResult SignupPregnant([FromBody] PregnantSignupDto request)
        {
            if (_context.PregnantUsers.Any(u => u.Email == request.Email))
            {
                return Conflict("Email already exists.");
            }

            var pregnantUser = new PregnantUser
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                DateOfPregnancy = request.DateOfPregnancy,
                ExpectedDateOfBirth = request.ExpectedDateOfBirth,
                DescriptionOfCondition = request.DescriptionOfCondition
            };

            _context.PregnantUsers.Add(pregnantUser);
            _context.SaveChanges();
            return Ok("Pregnant user signed up successfully.");
        }


    }
}
