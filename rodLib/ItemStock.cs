using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rodLib
{
    public class ItemStock : BaseModel
    {
        public int ItemID { get; set; }
        public double Quantity { get; set; }
        [ForeignKey("ItemID")]
        public Item Item { get; set; }
    }
}
