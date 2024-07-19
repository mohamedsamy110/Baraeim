namespace Graduation_Project.Models
{
    public class PredictionRecord
    {
        public int Id { get; set; }
        public string Features { get; set; }
        public int Prediction { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
