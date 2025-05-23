using BeltDash.Application.Interfaces;
using BeltDash.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BeltDash.Api.Extensions
{
    /// <summary>
    /// Extensions for IApplicationBuilder that enable applying database migrations.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Automatically applies any pending database migrations and seeds initial data when the application starts.
        /// </summary>
        /// <param name="app">Application builder</param>
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BeltDashDbContext>();
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

            // Apply any pending migrations
            context.Database.Migrate();
            Console.WriteLine("Database migrations applied successfully.");

            // Seed initial data
            DataSeeder.Initialize(context, passwordHasher);
            Console.WriteLine("Initial seed data loaded successfully.");
        }
    }
}
