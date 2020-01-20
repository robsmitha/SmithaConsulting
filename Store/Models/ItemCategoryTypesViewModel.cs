using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class ItemCategoryTypesViewModel
    {
        public List<ItemCategoryTypeModel> ItemCategoryTypes { get; set; }
        public ItemCategoryTypesViewModel(List<ItemCategoryTypeModel> itemCategoryTypes)
        {
            ItemCategoryTypes = itemCategoryTypes ?? new List<ItemCategoryTypeModel>();
        }
    }
}
