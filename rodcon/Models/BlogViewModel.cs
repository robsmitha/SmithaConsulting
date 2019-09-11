using rod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rodcon.Models
{
    public class BlogViewModel
    {
        public Blog Blog { get; set; }
        public List<BlogComment> Comments { get; set; }
    }
}
