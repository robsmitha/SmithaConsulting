using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataModeling
{
    public class BaseModel
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool Active { get; set; } = true;
        public DateTime? ModifiedTime { get; set; }
    }
}
