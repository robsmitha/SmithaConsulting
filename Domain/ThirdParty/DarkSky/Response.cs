using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ThirdParty.DarkSky
{
        public class Response
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string timezone { get; set; }
            public Currently currently { get; set; }
            public Minutely minutely { get; set; }
            public Hourly hourly { get; set; }
            public Daily daily { get; set; }
            public Flags flags { get; set; }
            public int offset { get; set; }
        }
}
