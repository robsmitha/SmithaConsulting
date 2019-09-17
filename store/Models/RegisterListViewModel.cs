using rod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models
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
