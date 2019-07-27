using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rodLib
{
    public class BaseModel
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public BaseModel()
        {
            CreatedAt = DateTime.Now;
            Active = true;
        }
    }
}
