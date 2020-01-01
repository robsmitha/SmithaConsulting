using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Discount : BaseModel
    {
        public decimal Percentage { get; set; }
        public decimal? Amount { get; set; }
        public string Name { get; set; }
    }
}
