using DomainLayer.Models;
using System.Collections.Generic;

namespace Store.Models
{
    public class RegisterListViewModel
    {
        public List<ItemModel> Items { get; set; }
        public RegisterListViewModel(List<ItemModel> items)
        {
            Items = items;
        }
    }
}
