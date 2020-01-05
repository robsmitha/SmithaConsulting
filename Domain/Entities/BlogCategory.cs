using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class BlogCategory : BaseModel
    {
        public int BlogCategoryTypeID { get; set; }
        public int BlogID { get; set; }

        [ForeignKey("BlogCategoryTypeID")]
        public BlogCategoryType BlogCategoryType { get; set; }

        [ForeignKey("BlogID")]
        public Blog Blog { get; set; }
    }
}
