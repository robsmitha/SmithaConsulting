using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rodcon.Models
{
    public class ContactViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Phone (Optional)")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }
    }
}
