using Microsoft.OpenApi.Models;

namespace BeltDash.Api.Extensions
{
    /// <summary>
    /// IServiceCollection extensions to configure services specific to the presentation layer (API).
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        // Define a constant for the API version to avoid repeated hardcoding
        private const string ApiVersion = "v1";

        /// <summary>
        /// Adds and configures Swagger/OpenAPI services.
        /// </summary>
        /// <param name="services">The application's service collection</param>
        /// <returns>The service collection with Swagger added</returns>
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Adds the API endpoint explorer
            services.AddEndpointsApiExplorer();

            // Swagger configuration for generating OpenAPI documentation
            services.AddSwaggerGen(options =>
            {
                // Configures the Swagger document with basic API information
                options.SwaggerDoc(ApiVersion, new OpenApiInfo
                {
                    Title = "Belt Dash API",                                  // API title
                    Version = ApiVersion,                                     // API version
                    Description = "RESTful API for the Belt Dash video game"  // API description
                });

                // Configures JWT (Bearer) security for the API
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",                                            // Authorization header name
                    Type = SecuritySchemeType.Http,                                    // Security scheme type
                    Scheme = "Bearer",                                                 // Security scheme
                    BearerFormat = "JWT",                                              // JWT token format
                    In = ParameterLocation.Header,                                     // Header location
                    Description = "JWT authorization header using the Bearer scheme."  // Security description
                });

                // Defines the security requirements that must be met for each request
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"  // Requires the "Bearer" scheme
                            }
                        },
                        Array.Empty<string>()  // No additional scopes required
                    }
                });

                // Configures including XML comments for Swagger documentation
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);  // Adds XML comments for additional details
            });

            return services;
        }
    }
}
