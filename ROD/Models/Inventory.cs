using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ROD.Models
{
    public class Inventory : BaseModel
    {
        [Required]
        [Display(Name = "Name")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string ItemDescription { get; set; }

        [Required]
        [Display(Name = "Inventory Type")]
        public int InventoryTypeID { get; set; }

        [Required]
        [Display(Name = "Merchant")]
        public int MerchantID { get; set; }

        [ForeignKey("MerchantID")]
        public Merchant Merchant { get; set; }

        [ForeignKey("InventoryTypeID")]
        public Merchant InventoryType { get; set; }
    }
}
