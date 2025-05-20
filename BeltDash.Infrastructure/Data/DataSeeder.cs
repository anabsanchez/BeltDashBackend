using BeltDash.Application.Interfaces;
using BeltDash.Domain.Entities;
using BeltDash.Domain.Enums;

namespace BeltDash.Infrastructure.Data
{
    /// <summary>
    /// Class responsible for seeding initial data into the database.
    /// </summary>
    public static class DataSeeder
    {
        /// <summary>
        /// Inserts a default administrator user if the users table is empty.
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="passwordHasher">Password hashing service</param>
        public static void Initialize(BeltDashDbContext context, IPasswordHasher passwordHasher)
        {
            // Exit if there are already registered users
            if (context.Users.Any())
                return;

            // Create default administrator user
            var adminUser = new User
            {
                Username = "admin",
                Email = "admin@example.com",
                PasswordHash = passwordHasher.HashPassword("Admin123!"),
                Status = UserStatus.Active,
                RoleId = 2, // Assign 'admin' role
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Users.Add(adminUser);
            context.SaveChanges();
        }
    }
}
