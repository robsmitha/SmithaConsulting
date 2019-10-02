using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.Services.LanguageUnderstanding
{
    public class Entity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
    }
}
