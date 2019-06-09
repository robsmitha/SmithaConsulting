using Microsoft.EntityFrameworkCore;
using ROD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROD.Data
{
    /// <summary>
    /// The main class that coordinates Entity Framework functionality for a given data model is the "database context" class. 
    /// </summary>
    public class RODContext : DbContext
    {
        public RODContext(DbContextOptions<RODContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MerchantType> SalesChannels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<InventoryType> InventoryTypes { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>();
            modelBuilder.Entity<MerchantType>();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Merchant>();
            modelBuilder.Entity<InventoryType>();
            modelBuilder.Entity<Inventory>();

            modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())
                .ToList()
                .ForEach(r => r.DeleteBehavior = DeleteBehavior.Restrict);

        }
    }
}
