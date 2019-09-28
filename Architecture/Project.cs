using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Architecture
{
    public class Project : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int ProjectTypeID { get; set; }
        public int MerchantID { get; set; }
        [ForeignKey("ProjectTypeID")]
        public ProjectType ProjectType { get; set; }
        [ForeignKey("MerchantID")]
        public Merchant Merchant { get; set; }
    }
}
