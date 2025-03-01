namespace CareHaven.DTOS
{
    public class OrphanageDTO
    {
        public int OrphanageId { get; set; }
        public string OrphanageName { get; set; }
        public string Description { get; set; }
        public string Founder { get; set; }
        public DateTime EstablishmentDate { get; set; }
        public string Status { get; set; }
    }
}