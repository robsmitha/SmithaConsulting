using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ThirdParty.GoogleGeocode
{
    public class Response
    {
        public IList<Result> results { get; set; }
        public string status { get; set; }
    }
}
