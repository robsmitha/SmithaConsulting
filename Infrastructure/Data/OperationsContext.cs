using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data
{
    public class OperationsContext : DbContext
    {
        public OperationsContext(DbContextOptions<OperationsContext> options) : base(options) { }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }
        public DbSet<Authorization> Authorizations { get; set; }
        public DbSet<AuthorizationType> AuthorizationTypes { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogCategoryType> BlogCategoryTypes { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<BlogCommentReply> BlogCommentReplies { get; set; }
        public DbSet<BlogCommentStatusType> BlogCommentStatusTypes { get; set; }
        public DbSet<BlogStatusType> BlogStatusTypes { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<CashEvent> CashEvents { get; set; }
        public DbSet<CashEventType> CashEventTypes { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<CustomerCard> CustomerCards { get; set; }
        public DbSet<CustomerPhone> CustomerPhones { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<ItemCategoryType> ItemCategoryTypes { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<ItemModifier> ItemModifiers { get; set; }
        public DbSet<ItemModifierType> ItemModifierTypes { get; set; }
        public DbSet<ItemStock> ItemStock { get; set; }
        public DbSet<ItemTag> ItemTags { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<MerchantType> MerchantTypes { get; set; }
        public DbSet<MerchantUser> MerchantUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatusType> OrderStatusTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentStatusType> PaymentStatusTypes { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }
        public DbSet<PriceType> PriceTypes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskPriorityType> TaskPriorityTypes { get; set; }
        public DbSet<TaskStatusType> TaskStatusType { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<TaxRate> TaxRates { get; set; }
        public DbSet<TaxType> TaxTypes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamType> TeamTypes { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<UnitType> UnitTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VoidReasonType> VoidReasonTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressType>();
            modelBuilder.Entity<Application>();
            modelBuilder.Entity<ApplicationType>();
            modelBuilder.Entity<Authorization>();
            modelBuilder.Entity<AuthorizationType>();
            modelBuilder.Entity<Blog>();
            modelBuilder.Entity<BlogCategory>();
            modelBuilder.Entity<BlogCategoryType>();
            modelBuilder.Entity<BlogComment>();
            modelBuilder.Entity<BlogCommentReply>();
            modelBuilder.Entity<BlogCommentStatusType>();
            modelBuilder.Entity<BlogStatusType>();
            modelBuilder.Entity<CardType>();
            modelBuilder.Entity<CashEvent>();
            modelBuilder.Entity<CashEventType>();
            modelBuilder.Entity<County>();
            modelBuilder.Entity<Credit>();
            modelBuilder.Entity<Customer>();
            modelBuilder.Entity<CustomerAddress>();
            modelBuilder.Entity<CustomerCard>();
            modelBuilder.Entity<CustomerPhone>();
            modelBuilder.Entity<Discount>();
            modelBuilder.Entity<Item>();
            modelBuilder.Entity<ItemCategory>();
            modelBuilder.Entity<ItemCategoryType>();
            modelBuilder.Entity<ItemImage>();
            modelBuilder.Entity<ItemModifier>();
            modelBuilder.Entity<ItemModifierType>();
            modelBuilder.Entity<ItemStock>();
            modelBuilder.Entity<ItemTag>();
            modelBuilder.Entity<ItemType>();
            modelBuilder.Entity<LineItem>();
            modelBuilder.Entity<Merchant>();
            modelBuilder.Entity<MerchantType>();
            modelBuilder.Entity<MerchantUser>();
            modelBuilder.Entity<Order>();
            modelBuilder.Entity<OrderStatusType>();
            modelBuilder.Entity<Payment>();
            modelBuilder.Entity<PaymentStatusType>();
            modelBuilder.Entity<PaymentType>();
            modelBuilder.Entity<Permission>();
            modelBuilder.Entity<PhoneType>();
            modelBuilder.Entity<PriceType>();
            modelBuilder.Entity<Project>();
            modelBuilder.Entity<ProjectType>();
            modelBuilder.Entity<Refund>();
            modelBuilder.Entity<Role>();
            modelBuilder.Entity<RolePermission>();
            modelBuilder.Entity<State>();
            modelBuilder.Entity<Tag>();
            modelBuilder.Entity<TaskItem>();
            modelBuilder.Entity<TaskPriorityType>();
            modelBuilder.Entity<TaskStatusType>();
            modelBuilder.Entity<TaskType>();
            modelBuilder.Entity<TaxRate>();
            modelBuilder.Entity<TaxType>();
            modelBuilder.Entity<Team>();
            modelBuilder.Entity<TeamType>();
            modelBuilder.Entity<Theme>();
            modelBuilder.Entity<UnitType>();
            modelBuilder.Entity<User>();
            modelBuilder.Entity<VoidReasonType>();

            modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())
                .ToList()
                .ForEach(r => r.DeleteBehavior = DeleteBehavior.Restrict);
        }
    }
}
