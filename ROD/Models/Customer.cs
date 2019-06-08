using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROD.Models
{
    public class Customer
    {
        /*
         * The ID property will become the primary key column of the database table that corresponds to this class. 
         * By default, the Entity Framework interprets a property that's named ID or classnameID as the primary key.
         */
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
