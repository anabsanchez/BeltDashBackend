using BeltDash.Api.Extensions;
using BeltDash.Api.Middleware;
using BeltDash.Application.Extensions;
using BeltDash.Infrastructure.Extensions;
using System.Text.Json.Serialization;

/// <summary>
/// The main class that represents the entry point of the application.
/// </summary>
public class Program
{
    /// <summary>
    /// The main method that configures services, middleware, and runs the application.
    /// </summary>
    /// <param name="args">Command-line arguments</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configures controllers and JSON options
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // Configures JSON serialization:
                // - Serializes enums as string values
                // - Ignores cyclic references in object relationships
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        // Configures Swagger documentation services
        builder.Services.AddSwaggerServices();

        // Adds services from the application layer
        builder.Services.AddApplicationServices(builder.Configuration);

        // Adds services from the infrastructure layer
        builder.Services.AddInfrastructureServices(builder.Configuration);

        // Configures CORS policy to allow requests from any origin
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        // Builds the application
        var app = builder.Build();

        // Configures the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            // Enables Swagger only in the development environment
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Registers the middleware for centralized exception handling
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        // Redirects HTTP requests to HTTPS
        app.UseHttpsRedirection();

        // Enables CORS with the defined policy
        app.UseCors("AllowAll");

        // Enables authentication and authorization
        app.UseAuthentication();
        app.UseAuthorization();

        // Maps the API controllers
        app.MapControllers();

        // Runs pending migrations and loads initial data
        app.ApplyMigrations();

        // Starts the application
        app.Run();
    }
}
