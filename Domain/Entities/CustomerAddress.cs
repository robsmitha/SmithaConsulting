using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomerAddress : BaseModel
    {
        [Required]
        public string Street { get; set; }

        public string Street2 { get; set; }

        [Display(Name = "Apt/Unit Number")]
        public string Secondary { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [ForeignKey("StateID")]
        public State State { get; set; }

        [Required]
        [ForeignKey("CountyID")]
        public County County { get; set; }

        [Required]
        [Display( Name = "Zip")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required]
        public bool Primary { get; set; }
        
        [Required]
        public bool International { get; set; }

        [Required]
        [ForeignKey("AddressTypeID")]
        public AddressType AddressType { get; set; }


    }
}
