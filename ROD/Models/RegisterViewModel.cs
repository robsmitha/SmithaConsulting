using ROD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROD.Models
{
    public class RegisterViewModel
    {
        public int CustomerID { get; set; }
        public int UserID { get; set; }
        public List<Item> Items { get; set; }
        public OrderViewModel CurrentOrder { get; set; }
        public int SelectedItemID { get; set; }
        public RegisterViewModel() { }
        public RegisterViewModel(List<Item> items)
        {
            Items = items;
        }
        public RegisterViewModel(List<Item> items, OrderViewModel currentOrder)
        {
            Items = items;
            CurrentOrder = currentOrder;
        }
    }
}
