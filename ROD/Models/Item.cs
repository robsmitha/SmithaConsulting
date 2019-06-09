using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ROD.Models
{
    public class Item : BaseModel
    {
        /// <summary>
        /// Name of the item
        /// </summary>
        [Required]
        [Display(Name = "Name")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string ItemDescription { get; set; }

        [Required]
        [Display(Name = "Item Type")]
        public int ItemTypeID { get; set; }

        [Required]
        [Display(Name = "Merchant")]
        public int MerchantID { get; set; }

        /// <summary>
        /// Cost of the item to merchant
        /// </summary>
        [Display(Name = "Cost")]
        public decimal Cost { get; set; }

        /// <summary>
        /// Price of the item
        /// </summary>
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        /// <summary>
        /// ['FIXED' or 'VARIABLE' or 'PER_UNIT']
        /// </summary>
        [Required]
        [Display(Name = "Price Type")]
        public int PriceTypeID { get; set; }

        /// <summary>
        /// Unit name, e.g. oz, lb
        /// </summary>
        [Required]
        [Display(Name = "Unit Type")]
        public int UnitTypeID { get; set; }

        /// <summary>
        /// Product code, e.g. UPC or EAN
        /// </summary>
        [Display(Name = "Code")]
        public int Code { get; set; }

        /// <summary>
        /// SKU of the item
        /// </summary>
        [Display(Name = "Sku")]
        public int Sku { get; set; }

        /// <summary>
        /// Flag to indicate whether or not to use default tax rates
        /// </summary>
        [Display(Name = "Use default tax rates")]
        public bool DefaultTaxRates { get; set; }

        /// <summary>
        /// True if this item should be counted as revenue, for example gift cards and donations would not
        /// </summary>
        public bool IsRevenue { get; set; }

        [ForeignKey("MerchantID")]
        public Merchant Merchant { get; set; }

        [ForeignKey("ItemTypeID")]
        public ItemType ItemType { get; set; }

        [ForeignKey("UnitTypeID")]
        public UnitType UnitType { get; set; }

        [ForeignKey("PriceTypeID")]
        public PriceType PriceType { get; set; }
    }
}
