using BeltDash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeltDash.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Specific configuration for the User entity in Entity Framework Core.
    /// Implements the Fluent API pattern to configure the entity.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configures the mapping of the User entity to the corresponding database table.
        /// Defines constraints, indexes, and relationships.
        /// </summary>
        /// <param name="builder">Entity type builder used to configure the entity</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Define the primary key
            builder.HasKey(u => u.Id);

            // Configure the Username property as required with a maximum length
            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(25);

            // Configure the Email property as required with a maximum length
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            // Configure the PasswordHash property as required
            builder.Property(u => u.PasswordHash)
                .IsRequired();

            // Create a unique index on Username for efficient lookups and uniqueness enforcement
            builder.HasIndex(u => u.Username)
                .IsUnique();

            // Create a unique index on Email for efficient lookups and uniqueness enforcement
            builder.HasIndex(u => u.Email)
                .IsUnique();

            // Define the relationship with the Role entity
            builder.HasOne(u => u.Role)             // A User has a Role
                .WithMany(r => r.Users)             // That Role has many Users
                .HasForeignKey(u => u.RoleId)       // The FK is in User
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete (shared or hierarchical reference relationship)
        }
    }
}
