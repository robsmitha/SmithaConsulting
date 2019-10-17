using System;
using System.Collections.Generic;
using System.Text;

namespace DataModeling
{
    public class Theme : BaseModel
    {
        public string Name { get; set; }
        public string StyleSheetCDN { get; set; }
    }
}
