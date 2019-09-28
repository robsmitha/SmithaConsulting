using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Architecture
{
    public class ProjectType : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
