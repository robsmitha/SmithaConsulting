using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Architecture
{
    public class ItemModifierType : BaseModel
    {
        public int MaxAllowed { get; set; }
        public int MinRequired { get; set; }
        public int SortOrder { get; set; }
        public string Name { get; set; }
        public bool DefaultModifier { get; set; }
        public int ItemID { get; set; }
        [ForeignKey("ItemID")]
        public Item Item { get; set; }
    }
}
