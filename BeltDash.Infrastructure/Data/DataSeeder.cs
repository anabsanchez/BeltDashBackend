using BeltDash.Application.Interfaces;
using BeltDash.Domain.Entities;
using BeltDash.Domain.Enums;

namespace BeltDash.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void Initialize(BeltDashDbContext context, IPasswordHasher passwordHasher)
        {
            if (context.Users.Any())
                return;

            var adminUser = new User
            {
                Username = "admin",
                Email = "admin@example.com",
                PasswordHash = passwordHasher.HashPassword("Admin123!"),
                Status = UserStatus.Active,
                RoleId = 2, 
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.Users.Add(adminUser);
            context.SaveChanges();
        }
    }
}
