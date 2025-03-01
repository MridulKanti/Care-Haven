using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
namespace CareHaven.Models
{
    public class Donation
    {
        public int DonationId { get; set; }
        [ForeignKey("Orphanage")]
        public int? OrphanageId { get; set; }
        // [JsonIgnore]
        public Orphanage? Orphanage { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonationDate { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        // [JsonIgnore]
        public User? User { get; set; }
    }
}
