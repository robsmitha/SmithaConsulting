using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Architecture
{
    public class CustomerPhone : BaseModel
    {
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }

        [ForeignKey("PhoneTypeID")]
        public PhoneType PhoneType { get; set; }

        [Required]
        public bool Default { get; set; }
    }
}