using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ROD.Models
{
    public class Order : BaseModel
    {
        public string Note { get; set; }
        public decimal Total { get; set; }
        public int OrderStatusTypeID { get; set; }

        [ForeignKey("OrderStatusTypeID")]
        public OrderStatusType OrderStatusType { get; set; }
    }
}
