using ROD.Data;
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
            if (!context.Database.EnsureCreated())
                return;

            var customers = new List<Customer>
            {
                new Customer
                {
                    FirstName = "Dim",
                    MiddleName = "Lynn",
                    LastName = "Tvyn",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    Email = "Dim@Tvyn.com",
                    Password = string.Empty
                }
            };

            context.Customers.AddRange(customers);

            var merchantTypes = new List<MerchantType>
            {
                new MerchantType
                {
                    Name = "Online",
                    Description = "Online Sale on merchant's behalf",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                }
            };

            context.MerchantTypes.AddRange(merchantTypes);

            var users = new List<User>
            {
                new User
                {
                    FirstName = "Rob",
                    MiddleName = string.Empty,
                    LastName = "Smitha",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    Email = "rob.smitha@email.com",
                    Password = string.Empty,
                    Username = "rob.smitha"
                }
            };

            context.Users.AddRange(users);

            context.SaveChanges();

            var user = users.FirstOrDefault();
            var merchantType = merchantTypes.FirstOrDefault();
            var merchants = new List<Merchant>
            {
                new Merchant
                {
                    MerchantName = "Online Sales Merchant",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    WebsiteUrl = string.Empty,
                    MerchantType = merchantType,
                    MerchantTypeID = merchantType.ID,
                    OwnerUser = user,
                    OwnerUserID = user.ID
                }
            };

            context.Merchants.AddRange(merchants);

            context.SaveChanges();
        }
    }
}
