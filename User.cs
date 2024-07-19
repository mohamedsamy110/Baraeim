using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        [Required]
        public double BabyWeight { get; set; }
        [Required]
        public double ChildLength { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string Description { get; set; }
        public string? OTP { get; set; } 
        public DateTime OTPExpiration { get; set; }

    }
}
