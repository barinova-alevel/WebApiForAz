using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApiForAz.Middleware
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

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("Middleware running");

            try
            {
                await _next(context);
            }
            catch (DbUpdateException ex)
            {
                // Log without sensitive data
                _logger.LogError(ex, "Database update error occurred");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = "Invalid data. A required field may be missing or null." });
            }
            catch (ValidationException ex)
            {
                // Only log the validation message, not the entire exception which might contain sensitive data
                _logger.LogError("Validation error: {Message}", ex.Message);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the error without exposing sensitive details to the user
                _logger.LogError(ex, "An unexpected error occurred");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred. Please try again later." });
            }
        }
    }

}
