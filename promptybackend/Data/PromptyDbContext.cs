using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Prompty.Server.Data
{
    public class PromptyDbContext : DbContext
    {
        public PromptyDbContext(DbContextOptions<PromptyDbContext> options) : base(options)
        {
        }

        // DbSet for your custom application entities (if any)
        // public DbSet<YourEntity> YourEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize Identity tables (if needed)
            // Example: builder.Entity<ApplicationUser>().ToTable("CustomUsers");
            // ...

            // Configure relationships, indexes, etc.
            // Example: builder.Entity<YourEntity>().HasIndex(e => e.SomeProperty);
            // ...

            // Add any other custom model configurations
            // ...

            // Call base OnModelCreating to apply Identity configurations
            // (e.g., user roles, claims, etc.)
        }
    }

}

