using rod.Data;
using rod.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rod.Data
{
    public static class DbInitializer
    {
        public static void Initialize(rodContext context)
        {
            if (!context.Database.EnsureCreated())
            {
                return; //db was already created or error occurred
            }

            var merchantTypes = new List<MerchantType>
            {
                new MerchantType
                {
                    Name = "General Merchant",
                    Description = "General Merchant Default Value",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                },
                new MerchantType
                {
                    Name = "Online Merchant",
                    Description = "Online Merchant",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                }
            };

            foreach(var merchantType in merchantTypes)
            {
                context.MerchantTypes.Add(merchantType);
                context.SaveChanges();
            }

            var merchants = new List<Merchant>
            {
                new Merchant
                {
                    MerchantName = "Rob's Merchant",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    WebsiteUrl = string.Empty,
                    MerchantType = merchantTypes.FirstOrDefault(),
                    MerchantTypeID = merchantTypes.FirstOrDefault().ID
                }
            };
            context.Merchants.AddRange(merchants);

            var itemTypes = new List<ItemType>
            {
                new ItemType
                {
                    Name = "Merchandise",
                    Description = "Merchandise",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                },
                new ItemType
                {
                    Name = "Discount",
                    Description = "Discount",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                }
            };


            foreach (var itemType in itemTypes)
            {
                context.ItemTypes.Add(itemType);
                context.SaveChanges();
            }


            var priceTypes = new List<PriceType>
            {
                new PriceType
                {
                    Name = "Fixed",
                    Description = "Fixed Price",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                },
                new PriceType
                {
                    Name = "Variable",
                    Description = "Variable Cost Price",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                }
            };

            foreach (var priceType in priceTypes)
            {
                context.PriceTypes.Add(priceType);
                context.SaveChanges();
            }

            var unitTypes = new List<UnitType>
            {
                new UnitType
                {
                    Name = "Quantity",
                    Description = "Unit is measured in quantities",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                }
            };

            context.UnitTypes.AddRange(unitTypes);

            var roles = new List<Role>
            {
                new Role
                {
                    Name = "Owner",
                    Description = "Owner of the associated merchant",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                }
            };

            context.Roles.AddRange(roles);

            var orderStatusTypes = new List<OrderStatusType>
            {
               new OrderStatusType
               {
                   Name = "Open",
                    Description = "Order needs to be paid for",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
               },
               new OrderStatusType
               {
                   Name = "Paid",
                    Description = "Order has been paid for",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
               },
               new OrderStatusType
               {
                    Name = "Partially Paid",
                    Description = "Order has been partially paid for",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
               }
            };

            foreach (var orderStatusType in orderStatusTypes)
            {
                context.OrderStatusTypes.Add(orderStatusType);
                context.SaveChanges();
            }


            var paymentStatusTypes = new List<PaymentStatusType>
            {
               new PaymentStatusType
               {
                   Name = "Pending",
                    Description = "Payment is pending",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
               },
               new PaymentStatusType
               {
                   Name = "Paid",
                    Description = "Payment is paid for",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
               },
            };

            foreach (var paymentStatusType in paymentStatusTypes)
            {
                context.PaymentStatusTypes.Add(paymentStatusType);
                context.SaveChanges();
            }

            var paymentTypes = new List<PaymentType>
            {
               new PaymentType
               {
                   Name = "Credit Card Manual",
                    Description = "Credit Card Manually Entered",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
               },
               new PaymentType
               {
                   Name = "Cash",
                    Description = "Cash",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
               },
               new PaymentType
               {
                   Name = "Check",
                    Description = "Check",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
               },
            };

            foreach (var paymentType in paymentTypes)
            {
                context.PaymentTypes.Add(paymentType);
                context.SaveChanges();
            }

            var user = new User
            {
                Username = "rob.smitha",
                Email = "rob@rob.com",
                Password = "$RODHASH$V1$10000$lWhmzFsHcZTJElQhnfRBJoDWbMxIbf9uyak+WkG0fVDXyvdX",
                Active = true,
                CreatedAt = DateTime.Now,
                FirstName = "Rob",
                MiddleName = "Wayne",
                LastName = "Smitha",

            };
            context.Users.Add(user);

            var permissions = new List<Permission>
            {
                new Permission
                {
                     Name = "AccessFeatures",
                    Description = "Can Access Features",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                },
                new Permission
                {
                     Name = "AccessTypes",
                    Description = "Can Access Types",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                }
            };

            foreach (var permission in permissions)
            {
                context.Permissions.Add(permission);
                context.SaveChanges();
            }

            var roleId = roles.FirstOrDefault().ID;
            var rolePermissions = new List<RolePermission>();
            foreach (var permission in permissions)
            {
                rolePermissions.Add(new RolePermission
                {
                    PermissionID = permission.ID,
                    RoleID = roleId,
                    Active = true,
                    CreatedAt = DateTime.Now
                });
            }
            context.RolePermissions.AddRange(rolePermissions);
            context.SaveChanges();
            var merchant = merchants.FirstOrDefault();
            if(merchant != null)
            {
                var merchantUser = new MerchantUser
                {
                    UserID = user.ID,
                    MerchantID = merchant.ID,
                    RoleID = roleId,
                    Active = true,
                    CreatedAt = DateTime.Now
                };
                context.MerchantUsers.Add(merchantUser);
                context.SaveChanges();
            }
        }
    }
}
