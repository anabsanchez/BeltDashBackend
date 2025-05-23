using BeltDash.Domain.Entities;
using BeltDash.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace BeltDash.Infrastructure.Data
{
    /// <summary>
    /// Database context for Entity Framework Core.
    /// Acts as a bridge between domain entities and the database.
    /// </summary>
    public class BeltDashDbContext : DbContext
    {
        /// <summary>
        /// Constructor that receives configuration options for the context.
        /// </summary>
        /// <param name="options">Configuration options for the database context</param>
        public BeltDashDbContext(DbContextOptions<BeltDashDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// DbSet representing the Users table in the database.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// DbSet representing the Roles table in the database.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// DbSet representing the Scores table in the database.
        /// </summary>
        public DbSet<Score> Scores { get; set; }

        /// <summary>
        /// Configures the data model when the context is being created.
        /// Defines relationships, constraints, and seed data.
        /// </summary>
        /// <param name="modelBuilder">Model builder used to configure the context</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Applies entity configurations defined in the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BeltDashDbContext).Assembly);

            // Fixed date to avoid issues with dynamic data in migrations
            var seedDate = new DateTime(2025, 01, 01, 0, 0, 0, DateTimeKind.Utc);

            // Initial seed data for system roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "player", CreatedAt = seedDate, UpdatedAt = seedDate },
                new Role { Id = 2, Name = "admin", CreatedAt = seedDate, UpdatedAt = seedDate }
            );
        }

        /// <summary>
        /// Overrides SaveChangesAsync to automatically update timestamp fields.
        /// Implements an aspect of the entity auditing pattern.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the asynchronous operation if needed</param>
        /// <returns>Number of entities affected in the database</returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Updates timestamp fields for entities being added or modified
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
