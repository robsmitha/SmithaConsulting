using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ItemModifier : BaseModel
    {
        /// <summary>
        /// Additional cost when used
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Name of the modifier
        /// </summary>
        public string Name { get; set; }

        public int ItemModifierTypeID { get; set; }

        [ForeignKey("ItemModifierTypeID")]
        public ItemModifierType ItemModifierType { get; set; }
    }
}
