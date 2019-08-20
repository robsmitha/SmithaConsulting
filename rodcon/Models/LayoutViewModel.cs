using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rodcon.Models
{
    public class LayoutViewModel
    {
        public string Greeting { get; set; } = string.Empty;
        public bool IsLoggedIn { get; set; } = false;
        public int? UserId { get; set; }
        public bool CanAccessFeatures { get; set; } = true;
        public bool CanAccessTypes { get; set; } = true;
    }
}
