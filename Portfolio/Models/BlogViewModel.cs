using System;

namespace Portfolio.Models
{
    public class BlogViewModel
    {
        public int ID { get; set; }
        public string Author { get; set; }
        public string AuthorEmail { get; set; }
        public string Title { get; set; }
        public string Subheading { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int BlogStatusTypeID { get; set; }
        public DateTime? PublishDate { get; set; }
        public int UserID { get; set; }
    }
}
