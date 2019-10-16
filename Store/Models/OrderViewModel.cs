using Architecture;
using Architecture.DTOs;
using Architecture.Services.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class OrderViewModel
    {
        public OrderDTO Order { get; set; }
        public List<LineItemDTO> LineItems { get; set; }
        public List<PaymentDTO> Payments { get; set; }
        public OrderViewModel() { }
        public OrderViewModel(OrderDTO order, List<LineItemDTO> lineItems, List<PaymentDTO> payments)
        {
            Order = order;
            LineItems = lineItems;
            Payments = payments;
        }
    }
}
