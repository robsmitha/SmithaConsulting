using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ThirdParty.DarkSky
{
    public class Flags
    {
        public IList<string> sources { get; set; }
        public double nearest_station { get; set; }
        public string units { get; set; }
    }
}
