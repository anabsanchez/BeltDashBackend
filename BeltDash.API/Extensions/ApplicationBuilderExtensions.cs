using BeltDash.Application.Interfaces;
using BeltDash.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BeltDash.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BeltDashDbContext>();
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

            context.Database.Migrate();
            Console.WriteLine("Database migrations applied successfully.");

            DataSeeder.Initialize(context, passwordHasher);
            Console.WriteLine("Initial seed data loaded successfully.");
        }
    }
}
