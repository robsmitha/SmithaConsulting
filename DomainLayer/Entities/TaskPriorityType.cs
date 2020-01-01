using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class TaskPriorityType : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
