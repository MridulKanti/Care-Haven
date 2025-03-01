namespace CareHaven.DTOS
{
    public class DonationDTO
    {
        public int DonationId { get; set; }
        public int? OrphanageId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonationDate { get; set; }
        public int UserId { get; set; }
    }
}
