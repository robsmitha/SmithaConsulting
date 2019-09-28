using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Models
{
    public class ThemeListViewModel
    {
        public string version { get; set; }
        public string CurrentTheme { get; set; }
        public IEnumerable<ThemeViewModel> themes { get; set; }
        public ThemeListViewModel() { }
    }
}
