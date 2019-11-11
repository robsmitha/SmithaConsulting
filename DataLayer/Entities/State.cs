using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer
{
    public class State : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }
    }
}
