using BeltDash.Domain.Interfaces;
using BeltDash.Domain.Interfaces.Common;
using BeltDash.Infrastructure.Data;
using BeltDash.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeltDash.Infrastructure.Extensions
{
    /// <summary>
    /// Static class providing extension methods to configure infrastructure services.
    /// Implements the centralized dependency registration pattern.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all infrastructure services into the dependency injection container.
        /// Configures the database context and repositories.
        /// </summary>
        /// <param name="services">Application service collection</param>
        /// <param name="configuration">Application configuration</param>
        /// <returns>The service collection with added services</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Retrieve the connection string from the configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            // Retrieve the full name of the assembly containing the migrations
            var migrationsAssembly = typeof(BeltDashDbContext).Assembly.FullName
                ?? throw new InvalidOperationException("Unable to retrieve the migration assembly name.");

            // Register the database context with SQL Server and specify the migration assembly
            services.AddDbContext<BeltDashDbContext>(options =>
                options.UseSqlServer(connectionString, sqlOptions =>
                    sqlOptions.MigrationsAssembly(migrationsAssembly)));

            // Register repositories in the dependency injection container
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IScoreRepository, ScoreRepository>();

            // Register the UnitOfWork in the dependency injection container
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
