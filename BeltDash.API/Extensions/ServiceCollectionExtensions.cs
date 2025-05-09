using Microsoft.OpenApi.Models;

namespace BeltDash.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string ApiVersion = "v1";

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ApiVersion, new OpenApiInfo
                {
                    Title = "Belt Dash API",
                    Version = ApiVersion,
                    Description = "RESTful API for the Belt Dash video game"  
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

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"  
                            }
                        },
                        Array.Empty<string>()  
                    }
                });

                // Configures including XML comments for Swagger documentation
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);  
            });

            return services;
        }
    }
}
