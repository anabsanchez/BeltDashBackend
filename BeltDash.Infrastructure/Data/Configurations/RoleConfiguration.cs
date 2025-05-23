using BeltDash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeltDash.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Specific configuration for the Role entity in Entity Framework Core.
    /// Implements the Fluent API pattern to configure the entity.
    /// </summary>
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        /// <summary>
        /// Configures the mapping of the Role entity to the corresponding database table.
        /// Defines constraints, indexes, and relationships.
        /// </summary>
        /// <param name="builder">Entity type builder used to configure the entity</param>
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Define the primary key
            builder.HasKey(r => r.Id);

            // Configure the Name property as required with a maximum length
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Create a unique index on Name for efficient lookups and uniqueness enforcement
            builder.HasIndex(r => r.Name)
                .IsUnique();
        }
    }
}
