using DomainLayer.Models;
using System.Collections.Generic;

namespace Store.Models
{
    public class StoreListViewModel
    {
        public List<ItemModel> Items { get; set; }
        public StoreListViewModel(List<ItemModel> items)
        {
            Items = items;
        }
    }
}
