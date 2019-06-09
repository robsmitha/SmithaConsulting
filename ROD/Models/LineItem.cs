using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ROD.Models
{
    public class LineItem : BaseModel
    {
        public decimal ItemAmount { get; set; }
        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public Order Order { get; set; }
    }
}
