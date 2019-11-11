using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer
{
    public class BlogComment : BaseModel
    {
        public string Comment { get; set; }
        public int UserID { get; set; }
        public int BlogID { get; set; }
        public int BlogCommentStatusTypeID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("BlogID")]
        public Blog Blog { get; set; }

        [ForeignKey("BlogCommentStatusTypeID")]
        public BlogCommentStatusType BlogCommentStatusType { get; set; }
    }
}
