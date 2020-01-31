using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ThirdParty.DarkSky
{
    public class Hourly
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public IList<Data> data { get; set; }
    }
}
