using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROD.Models
{
    public class BaseModel
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
        public DateTime ModifiedTime { get; set; }
    }
}
