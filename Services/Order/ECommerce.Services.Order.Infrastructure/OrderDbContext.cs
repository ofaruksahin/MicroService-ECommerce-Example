using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Order.Infrastructure
{
    public class OrderDbContext : DbContext
    {

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }

        public DbSet<Domain.OrderAggregate.Order> Orders { get; set; }
        public DbSet<Domain.OrderAggregate.OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.OrderAggregate.Order>()
                .ToTable("Orders");

            modelBuilder.Entity<Domain.OrderAggregate.OrderItem>()
                .ToTable("OrderItems");

            modelBuilder.Entity<Domain.OrderAggregate.OrderItem>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Domain.OrderAggregate.Order>()
                .OwnsOne(o => o.Address)
                .WithOwner();

            base.OnModelCreating(modelBuilder);
        }
    }
}
