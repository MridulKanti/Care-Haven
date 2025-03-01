namespace CareHaven.DTOS
{
    public class FeedbackDTO
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public string FeedbackText { get; set; }
        public DateTime Date { get; set; }
    }
}