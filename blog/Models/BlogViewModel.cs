using rod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Models
{
    public class BlogViewModel
    {
        public Blog Blog { get; set; }
        public List<BlogComment> Comments { get; set; }
    }
}
