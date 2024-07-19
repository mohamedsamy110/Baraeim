namespace Graduation_Project.Models
{
    public class Homepregnant
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfPregnancy { get; set; }
        public int FetusWeeksAge { get; set; }
        public double ExpectedLength { get; set; }
        public double ExpectedWeight { get; set; }

    }
}
