using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ItemCategoryTypeModel
    {
        public int ID { get; set; }
        public bool Active { get; set; } = true;
        public int SortOrder { get; set; }
        public string Name { get; set; }
    }
}
