using System.Text.Json;
using BeltDash.Application.DTOs.Common;
using BeltDash.Domain.Exceptions;

namespace BeltDash.Api.Middleware
{
    /// <summary>
    /// Middleware for centralized exception handling in the application.
    /// Converts exceptions into structured API responses.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Constructor for the exception handling middleware.
        /// </summary>
        /// <param name="next">Next middleware in the pipeline</param>
        /// <param name="logger">Logger to record exceptions</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware and catches any unhandled exceptions.
        /// </summary>
        /// <param name="context">HTTP context</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Pass the request to the next middleware
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An unhandled exception occurred.");
                // Handle the exception by converting it into an API response
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Converts the exception into a structured API response.
        /// Sets the appropriate HTTP status code based on the exception type.
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <param name="exception">Caught exception</param>
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Determine response based on exception type
            var response = exception switch
            {
                // Domain exceptions (validations, business rules)
                DomainException domainException =>
                    ApiResponseDto<object>.ErrorResponse(domainException.Message, StatusCodes.Status400BadRequest),

                // Unauthorized access exceptions
                UnauthorizedAccessException =>
                    ApiResponseDto<object>.ErrorResponse("Unauthorized access", StatusCodes.Status401Unauthorized),

                // Resource not found exceptions
                KeyNotFoundException =>
                    ApiResponseDto<object>.ErrorResponse("Resource not found", StatusCodes.Status404NotFound),

                // Any other exception is treated as internal server error
                _ =>
                    ApiResponseDto<object>.ErrorResponse("An unexpected error occurred", StatusCodes.Status500InternalServerError)
            };

            // Set the HTTP status code
            context.Response.StatusCode = response.StatusCode;

            // Serialize and write the response
            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
