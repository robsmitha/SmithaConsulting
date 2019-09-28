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

            var general = new MerchantType
            {
                Name = "General Merchant",
                Description = "General Merchant Default Value",
                CreatedAt = DateTime.UtcNow,
                Active = true,
            };
            var online = new MerchantType
            {
                Name = "Online Merchant",
                Description = "Online Merchant",
                CreatedAt = DateTime.UtcNow,
                Active = true,
            };
            var merchantTypes = new List<MerchantType>
            {
                general,
                online
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
                    MerchantType = general,
                    MerchantTypeID = general.ID
                },
                new Merchant
                {
                    MerchantName = "Online Merchant",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    WebsiteUrl = string.Empty,
                    MerchantType = online,
                    MerchantTypeID = online.ID
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
            var owner = new Role
            {
                Name = "Owner",
                Description = "Owner of the associated merchant",
                CreatedAt = DateTime.UtcNow,
                Active = true,
            };
            var onlineSignUp = new Role
            {
                Name = "Online User",
                Description = "Online Sign up user",
                CreatedAt = DateTime.UtcNow,
                Active = true,
            };
            var roles = new List<Role>
            {
                owner,
                onlineSignUp
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
                Email = "robsmitha94@gmail.com",
                Password = "$RODHASH$V1$10000$3uUrCcaVxkjPFv6ZodK6vMchU8v5P8cuL4U0A2c/Rvxypb3l",
                Active = true,
                CreatedAt = DateTime.Now,
                FirstName = "Rob",
                MiddleName = "Wayne",
                LastName = "Smitha",
            };
            context.Users.Add(user);

            var @namespace = "rod";
            var entities = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                       .Where(t => t.IsClass && t.Namespace == @namespace 
                       && t.Name != "BaseModel");
            var entityList = new List<string>();


            var permissions = new List<Permission>();
            foreach (var entity in entities)
            {
                var contollerName = entity.Name;
                if(contollerName != "Blog")
                {
                    var lastLetter = contollerName.Substring(contollerName.Length - 1);
                    switch (lastLetter)
                    {
                        case "y":
                            contollerName = $"{entity.Name.TrimEnd('y')}ies";
                            break;
                        case "s":
                            contollerName = $"{entity.Name}es";
                            break;
                        default:
                            contollerName = $"{entity.Name}s";
                            break;
                    }
                }
                var access = new Permission
                {
                    Name = $"Access{contollerName}",
                    Description = $"Can Access List {contollerName}",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    Controller = contollerName,
                    Action = "Index"
                };
                permissions.Add(access);
                var create = new Permission
                {
                    Name = $"Create{contollerName}",
                    Description = $"Can Create {contollerName}",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    Controller = contollerName,
                    Action = "Create"
                };
                permissions.Add(create);
                var edit = new Permission
                {
                    Name = $"Edit{contollerName}",
                    Description = $"Can Edit {contollerName}",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    Controller = contollerName,
                    Action = "Edit"
                };
                permissions.Add(edit);
                var details = new Permission
                {
                    Name = $"Details{contollerName}",
                    Description = $"Can Access Details {contollerName}",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    Controller = contollerName,
                    Action = "Details"
                };
                permissions.Add(details);
                var delete = new Permission
                {
                    Name = $"Delete{contollerName}",
                    Description = $"Can Delete {contollerName}",
                    CreatedAt = DateTime.UtcNow,
                    Active = true,
                    Controller = contollerName,
                    Action = "Delete"
                };
                permissions.Add(delete);
            }
            context.Permissions.AddRange(permissions);
            context.SaveChanges();

            var roleId = roles.FirstOrDefault(x => x.Name == "Owner").ID;
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

            var blogStatusTypes = new List<BlogStatusType>
            {
                new BlogStatusType
                {
                    Name = "Drafted",
                    Description = "Drafted",
                    Active = true,
                    CreatedAt = DateTime.Now
                },
                new BlogStatusType
                {
                    Name = "Published",
                    Description = "Published",
                    Active = true,
                    CreatedAt = DateTime.Now
                }
            };
            context.BlogStatusTypes.AddRange(blogStatusTypes);
        }
    }
}
