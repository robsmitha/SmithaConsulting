using Newtonsoft.Json;
using Administration.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Administration.Models
{
    public class StockViewModel
    {
        
        public StockViewModel(string jsonPayload)
        {
            dynamic stock = JsonConvert.DeserializeObject(jsonPayload);
            var information = stock["Meta Data"]["1. Information"];
            var symbol = stock["Meta Data"]["2. Symbol"];
            var lastRefreshed = stock["Meta Data"]["3. Last Refreshed"];
            var interval = stock["Meta Data"]["4. Interval"];
            var outputSize = stock["Meta Data"]["5. Output Size"];
            var timeZone = stock["Meta Data"]["6. Time Zone"];
            var timeSeries = stock[$"Time Series ({interval})"][lastRefreshed.ToString()];
            var open = timeSeries["1. open"];
            var high = timeSeries["2. high"];
            var low = timeSeries["3. low"];
            var close = timeSeries["4. close"];
            var volume = timeSeries["5. volume"];

            Information = information;
            Symbol = symbol;
            LastRefreshed = Convert.ToDateTime(lastRefreshed);
            Interval = interval;
            OutputSize = outputSize;
            TimeZone = timeZone;
            Open = Convert.ToDecimal(open);
            High = Convert.ToDecimal(high);
            Low = Convert.ToDecimal(low);
            Close = Convert.ToDecimal(close);
            Volume = Convert.ToInt32(volume);
        }
        public string Information { get; set; }
        public string Symbol { get; set; }
        public DateTime? LastRefreshed { get; set; }
        public string Interval { get; set; }
        public string OutputSize { get; set; }
        public string TimeZone { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public int Volume { get; set; }
    }
}
