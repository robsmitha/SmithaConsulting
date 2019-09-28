using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Models
{
    public class ThemeViewModel
    {
        public const string BaseUrl = "https://bootswatch.com/api/4.json";
        public string name { get; set; }
        public string description { get; set; }
        public string preview { get; set; }
        public string thumbnail { get; set; }
        public string css { get; set; }
        public string cssMin { get; set; }
        public string cssCdn { get; set; }
        public string scss { get; set; }
        public string scssVariables { get; set; }
        public ThemeViewModel() { }
    }
}
