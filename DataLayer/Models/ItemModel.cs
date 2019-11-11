using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using DataLayer;

namespace DataLayer.Models
{
    public class ItemModel
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int ItemTypeID { get; set; }
        public int MerchantID { get; set; }
        public decimal? Price { get; set; }
        public int PriceTypeID { get; set; }
        public int UnitTypeID { get; set; }
        public string Code { get; set; }
        public string LookupCode { get; set; }
        public decimal? Percentage { get; set; }

        public ItemModel(Item item)
        {
            if (item != null)
            {
                ID = item.ID;
                CreatedAt = item.CreatedAt;
                MerchantID = item.MerchantID;
                ItemTypeID = item.ItemTypeID;
                PriceTypeID = item.PriceTypeID;
                Price = item.Price;
                ItemName = item.ItemName;
                ItemDescription = item.ItemDescription;
                UnitTypeID = item.UnitTypeID;
                Code = item.Code;
                LookupCode = item.LookupCode;
                Percentage = item.Percentage;
            }
        }
    }
}
