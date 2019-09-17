using rod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace store.Models
{
    public class RegisterCartViewModel
    {
        public OrderViewModel Order { get; set; }
        public RegisterCartViewModel() { }
        public RegisterCartViewModel(OrderViewModel order)
        {
            Order = order;
        }
    }
}
