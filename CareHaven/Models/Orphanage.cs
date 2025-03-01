using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CareHaven.Models
{
    public class Orphanage
    {

        [Key]
        public int OrphanageId { get; set; }

        [Required(ErrorMessage = "Orphanage Name is required")]
        public string OrphanageName { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Founder is required")]
        public string Founder { get; set; }

        [Required(ErrorMessage = "Date is required")]

        public DateTime EstablishmentDate { get; set; }

        [Required(ErrorMessage = "Status is required")]

        public string Status { get; set; }

        public ICollection<Donation> Donations { get; set; }
    }
}
