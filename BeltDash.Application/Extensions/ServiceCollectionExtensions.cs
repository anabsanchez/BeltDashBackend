using BeltDash.Application.Interfaces;
using BeltDash.Application.Services;
using BeltDash.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace BeltDash.Application.Extensions
{
    /// <summary>
    /// Extensions to configure application layer services.
    /// Facilitates dependency registration in the application setup.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all necessary services for the application layer.
        /// Configures AutoMapper, FluentValidation, application services,
        /// password hashing service, and JWT authentication.
        /// </summary>
        /// <param name="services">Service collection to extend</param>
        /// <param name="configuration">Application configuration</param>
        /// <returns>The service collection with registered dependencies</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register AutoMapper for entity and DTO mapping
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Register FluentValidation for data validation
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register application services with dependency injection
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IScoreService, ScoreService>();

            // Register password hashing service as singleton
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            // Register JWT token generation service
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            // Configure JWT authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,                      // Validate token issuer
                    ValidateAudience = true,                    // Validate token audience
                    ValidateLifetime = true,                    // Validate token expiration
                    ValidateIssuerSigningKey = true,            // Validate token signature
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new ArgumentNullException("JWT Key is not configured")))
                };
            });

            return services;
        }
    }
}
