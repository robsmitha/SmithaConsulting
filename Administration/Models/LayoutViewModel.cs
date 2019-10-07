﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Models
{
    public class LayoutViewModel
    {
        public string Greeting { get; set; } = string.Empty;
        public bool IsLoggedIn { get; set; } = false;
        public int? UserId { get; set; } = null;
        public int? MerchantId { get; set; } = null;
    }
}
