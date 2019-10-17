using System;
using System.Collections.Generic;
using System.Text;
using DataModeling;

namespace Architecture.DTOs
{
    public class ApplicationDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ApplicationTypeID { get; set; }
        public int ThemeID { get; set; }
        public ApplicationDTO() { }
        public ApplicationDTO(Application application)
        {
            if(application != null)
            {
                ID = application.ID;
                Name = application.Name;
                Description = application.Description;
                ApplicationTypeID = application.ApplicationTypeID;
                ThemeID = application.ThemeID;
            }
        }
    }
}
