using BeltDash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeltDash.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Specific configuration for the Score entity in Entity Framework Core.
    /// Implements the Fluent API pattern to configure the entity.
    /// </summary>
    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        /// <summary>
        /// Configures the mapping of the Score entity to the corresponding database table.
        /// Defines constraints, indexes, and relationships.
        /// </summary>
        /// <param name="builder">Entity type builder used to configure the entity</param>
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            // Define the primary key
            builder.HasKey(s => s.Id);

            // Configure the Points property as required
            builder.Property(s => s.Points)
                .IsRequired();

            // Define the relationship with the User entity
            builder.HasOne(s => s.User)            // A Score has a User
                .WithMany(u => u.Scores)           // That User has many Scores
                .HasForeignKey(s => s.UserId)      // The FK is in Score
                .OnDelete(DeleteBehavior.Cascade); // Enables cascade delete (strong dependency relationship)
        }
    }
}
