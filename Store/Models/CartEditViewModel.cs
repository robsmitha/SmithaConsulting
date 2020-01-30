using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class CartEditViewModel
    {
        public int ItemID { get; set; }
        public int? Quantity { get; set; }
    }
}
