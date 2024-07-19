namespace Graduation_Project.Dtos
{
    public class PregnantSignupDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfPregnancy { get; set; }
        public DateTime ExpectedDateOfBirth { get; set; }
        public string DescriptionOfCondition { get; set; }

    }
}
