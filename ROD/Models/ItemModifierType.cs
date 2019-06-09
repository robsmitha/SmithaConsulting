﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROD.Models
{
    public class ItemModifierType : BaseModel
    {
        public int MaxAllowed { get; set; }
        public int MinRequired { get; set; }
        public int SortOrder { get; set; }
        public string Name { get; set; }
        public bool DefaultModifier { get; set; }
    }
}
