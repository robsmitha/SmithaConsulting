using DomainLayer.Models;
using System.Collections.Generic;

namespace Store.Models
{
    public class InventoryViewModel
    {
        public List<ItemModel> Items { get; set; }
        public InventoryViewModel(List<ItemModel> items)
        {
            Items = items;
        }
    }
}
