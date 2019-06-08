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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }
    }
}
