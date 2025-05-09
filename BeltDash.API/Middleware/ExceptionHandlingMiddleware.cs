using System.Text.Json;
using BeltDash.Application.DTOs.Common;
using BeltDash.Domain.Exceptions;

namespace BeltDash.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

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
