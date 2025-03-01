using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareHaven.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }
        public string FeedbackText { get; set; }
        public DateTime Date { get; set; }
    }
}
