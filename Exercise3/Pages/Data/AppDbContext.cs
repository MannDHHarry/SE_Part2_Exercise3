using Exercise3.Pages.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise3.Pages.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Agent> Agent { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Prevent cascading delete for Item -> OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Item)
                .WithMany(i => i.OrderDetails)
                .HasForeignKey(od => od.ItemID)
                .OnDelete(DeleteBehavior.Restrict);

            // You can add similar rules for other FK relationships if needed
        }


    }


}
