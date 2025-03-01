using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CareHaven.Models
{
    public class User
    {
        //internal readonly object Feedbacks;

        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "User Role is required")]
        public string UserRole { get; set; }
        public ICollection<Donation> Donations { get; set; }

       public ICollection<Feedback> Feedbacks { get; set; }
    }
}
