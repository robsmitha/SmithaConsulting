using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Application : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ApplicationTypeID { get; set; }
        [ForeignKey("ApplicationTypeID")]
        public ApplicationType ApplicationType { get; set; }
        public int ThemeID { get; set; }
        [ForeignKey("ThemeID")]
        public Theme Theme { get; set; }
    }
}
