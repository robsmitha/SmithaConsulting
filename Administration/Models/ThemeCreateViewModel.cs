using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Models
{
    public class ThemeCreateViewModel
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Select Theme")]
        [Required]
        public string StyleSheetCDN { get; set; }
        
    }
}
