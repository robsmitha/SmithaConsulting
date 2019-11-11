using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer
{
    public class Blog : BaseModel
    {
        public string Title { get; set; }
        public string Subheading { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int BlogStatusTypeID { get; set; }
        public DateTime? PublishDate { get; set; }
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("BlogStatusTypeID")]
        public BlogStatusType BlogStatusType { get; set; }
    }
}
