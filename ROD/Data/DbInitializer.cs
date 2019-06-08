using ROD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROD.Data
{
    public static class DbInitializer
    {
        public static void Initialize(RODContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Customers.Any())
            {
                return;   // DB has been seeded
            }

            //Create some seed data
            var customers = new List<Customer>
            {
                new Customer { FirstName = "Dim", MiddleName = "Lynn", LastName = "Tvyn", CreateDate = DateTime.UtcNow }
            };

            context.Customers.AddRange(customers);

            context.SaveChanges();
        }
    }
}
