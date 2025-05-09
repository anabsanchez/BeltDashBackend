using BeltDash.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeltDash.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            var migrationsAssembly = typeof(BeltDashDbContext).Assembly.FullName
                ?? throw new InvalidOperationException("Unable to retrieve the migration assembly name.");

            services.AddDbContext<BeltDashDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.MigrationsAssembly(migrationsAssembly)));

            return services;
        }
    }
}
