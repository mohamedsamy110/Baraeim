namespace Graduation_Project.Dtos
{
    public class UpdateHomeDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfPregnancy { get; set; }
        public int FetusWeeksAge { get; set; }
        public double ExpectedLength { get; set; }
        public double ExpectedWeight { get; set; }

    }
}
