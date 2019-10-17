using DataModeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Models
{
    public class HomeViewModel
    {
        public decimal YearEarnings { get;set; }
        public decimal MonthEarnings { get; set; }
        public int YearOrderCount { get; set; }
        public int MonthOrderCount { get; set; }
    }
}
