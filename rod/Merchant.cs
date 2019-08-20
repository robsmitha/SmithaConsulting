using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rod
{
    public class Merchant : BaseModel
    {
        [Required]
        [Display(Name = "Merchant Name")]
        public string MerchantName { get; set; }

        [Display(Name = "Website Url")]
        public string WebsiteUrl { get; set; }

        [Required]
        [Display(Name = "Merchant Type")]
        public int MerchantTypeID { get; set; }

        public bool SelfBoardingApplication { get; set; }

        public bool IsBillable { get; set; }

        [ForeignKey("MerchantTypeID")]
        public MerchantType MerchantType { get; set; }
    }
}
