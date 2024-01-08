using System;
using System.Threading;
using System.Threading.Tasks;
using BookHaven.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookHaven.Persistence
{
    public class BookHavenDbContext : DbContext
    {
        public BookHavenDbContext(DbContextOptions<BookHavenDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
       // public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }


        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    foreach (var item in ChangeTracker.Entries<BaseEntity>())
        //    {
        //        switch (item.State)
        //        {
        //            case EntityState.Modified:
        //                item.Entity.UpdatedAt = DateTime.UtcNow;
        //                break;
        //            case EntityState.Added:
        //                item.Entity.Id = Guid.NewGuid().ToString();
        //                item.Entity.CreatedAt = DateTime.UtcNow;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    return await base.SaveChangesAsync(cancellationToken);
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Enable sensitive data logging
            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Book>().HasKey(b => b.Id);

            // Additional configurations for other entities can be added here

            base.OnModelCreating(modelBuilder);
        }

    }
}
