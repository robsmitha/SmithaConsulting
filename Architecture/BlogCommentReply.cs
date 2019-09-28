using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Architecture
{
    public class BlogCommentReply : BaseModel
    {
        public string Reply { get; set; }
        public int UserID { get; set; }
        public int BlogCommentID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("BlogCommentID")]
        public BlogComment BlogComment { get; set; }
    }
}
