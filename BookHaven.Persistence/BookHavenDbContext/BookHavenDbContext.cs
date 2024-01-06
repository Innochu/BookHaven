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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<BaseEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        item.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        item.Entity.Id = Guid.NewGuid().ToString();
                        item.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define primary key for the User entity
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            // Additional configurations for other entities can be added here

            base.OnModelCreating(modelBuilder);
        }
    }
}
