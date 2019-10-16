using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DTOs
{
    public class LineItemDTO
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal ItemAmount { get; set; }
        public int ItemID { get; set; }
        public int OrderID { get; set; }
        public string ItemName { get; set; }
        public int ItemTypeID { get; set; }
        public string ItemDescription { get; set; }

        public LineItemDTO(LineItem lineItem)
        {
            if (lineItem != null)
            {
                ID = lineItem.ID;
                CreatedAt = lineItem.CreatedAt;
                ItemAmount = lineItem.ItemAmount;
                ItemID = lineItem.ItemID;
                OrderID = lineItem.OrderID;
                ItemName = lineItem.Item?.ItemName;
                ItemTypeID = lineItem.Item?.ItemTypeID ?? 0;
                ItemDescription = lineItem.Item?.ItemDescription;
            }
        }

        public LineItemDTO() { }
    }
}
