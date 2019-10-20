using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataModeling
{
    public class ProjectType : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
