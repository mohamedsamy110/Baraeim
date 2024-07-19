namespace Graduation_Project.Dtos
{
    public class CreateProfileDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public double BabyWeight { get; set; }
        public double ChildLength { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
