using AppCore.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

// Piotr Bacior - WSEI Kraków

namespace WebApi.Middleware;

public class ProblemDetailsExceptionHandler(
    ProblemDetailsFactory factory, 
    ILogger<ProblemDetailsExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is ContactNotFoundException)
        {
            logger.LogInformation($"Exception handled: {exception.Message}");
            
            var problem = factory.CreateProblemDetails(
                context,
                StatusCodes.Status404NotFound, // Zmieniam na 404, bo to bardziej logiczne dla Not Found
                "Contact service error!",
                detail: exception.Message
            );
            
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(problem, cancellationToken);
            return true;
        }
        return false;
    }
}