using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Models
{
    public class RevenueSourcesViewModel
    {
        public string RevenueSourcesJSON { get; set; }
        public string LabelsJSON { get; set; }
        public string ColorsJSON { get; set; }
        public IEnumerable<string> Labels { get; set; }
        public IEnumerable<string> Colors { get; set; }
    }
}
