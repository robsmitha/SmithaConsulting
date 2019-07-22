using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROD
{
    public class ItemTag : BaseModel
    {
        public int TagID { get; set; }
        public int ItemID { get; set; }
        public Tag Tag { get; set; }
        public Item Item { get; set; }
    }
}
