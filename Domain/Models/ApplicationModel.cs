using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Domain.Models
{
    public class ApplicationModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ApplicationTypeID { get; set; }
        public int ThemeID { get; set; }
        public ApplicationModel() { }
    }
}
