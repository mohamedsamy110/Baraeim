namespace Graduation_Project.Models
{
    public class Video
    {

        public int Id { get; set; }
        public int DevelopmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; } 
        public Development Development { get; set; }
    }
}
