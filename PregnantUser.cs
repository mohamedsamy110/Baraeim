using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Models
{
    public class PregnantUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        public DateTime DateOfPregnancy { get; set; }
        [Required]
        public DateTime ExpectedDateOfBirth { get; set; }
        public string DescriptionOfCondition { get; set; }
    }
}
