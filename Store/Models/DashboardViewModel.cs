using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class DashboardViewModel
    {
        public decimal YearEarnings { get; set; }
        public decimal MonthEarnings { get; set; }
        public int YearOrderCount { get; set; }
        public int MonthOrderCount { get; set; }
    }
}
