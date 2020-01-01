using DomainLayer.Entities;
using System.Collections.Generic;

namespace Administration.Models
{
    public class RegisterListViewModel
    {
        public List<Item> Items { get; set; }
        public RegisterListViewModel(List<Item> items)
        {
            Items = items;
        }
    }
}
