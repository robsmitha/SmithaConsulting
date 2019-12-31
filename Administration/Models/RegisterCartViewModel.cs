using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Models
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
