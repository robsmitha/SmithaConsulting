using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ApplicationType : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
