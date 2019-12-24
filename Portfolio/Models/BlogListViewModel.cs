using System.Collections.Generic;

namespace Portfolio.Models
{
    public class BlogListViewModel
    {
        public IEnumerable<BlogViewModel> Blogs { get; set; }
        public BlogListViewModel(IEnumerable<BlogViewModel> blogs) 
        {
            Blogs = blogs;
        }
    }
}
