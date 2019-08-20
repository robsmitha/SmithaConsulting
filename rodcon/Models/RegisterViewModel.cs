using rod;
using System.Collections.Generic;

namespace rodcon.Models
{
    public class RegisterViewModel
    {
        public int CustomerID { get; set; }
        public int UserID { get; set; }
        public int SelectedItemID { get; set; }
        public int SelectedLineItemID { get; set; }
        public int? CurrentOrderID { get; set; }
        public List<Item> Items { get; set; }
        public OrderViewModel Order { get; set; }
        public RegisterViewModel() { }
        public RegisterViewModel(List<Item> items)
        {
            Items = items;
        }
        public RegisterViewModel(List<Item> items, OrderViewModel order)
        {
            Items = items;
            Order = order;
            CurrentOrderID = order != null && order?.Order != null
                    ? order?.Order.ID
                    : null;
        }
    }
}
