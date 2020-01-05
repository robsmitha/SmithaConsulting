using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ItemTaxRate : BaseModel
    {
        public int TaxRateID { get; set; }
        public int ItemID { get; set; }
        [ForeignKey("TaxRateID")]
        public TaxRate TaxRate { get; set; }
        [ForeignKey("ItemID")]
        public Item Item { get; set; }
    }
}
