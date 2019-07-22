using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ROD
{
    public class ItemCategory : BaseModel
    {
        public int ItemCategoryTypeID { get; set; }
        public int ItemID { get; set; }

        [ForeignKey("ItemCategoryTypeID")]
        public ItemCategoryType ItemCategoryType { get; set; }

        [ForeignKey("ItemID")]
        public Item Item { get; set; }
    }
}
