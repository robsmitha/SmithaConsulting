using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace rodLib
{
    public class CustomerCard : BaseModel
    {
        [Required]
        public string Last4 { get; set; }

        [Required]
        public string First6 { get; set; }

        [Display( Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Card Type")]
        public CardType CardType { get; set; }

        [Required]
        [Display( Name = "Expiration Date")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        public string Token { get; set; }

    }
}
