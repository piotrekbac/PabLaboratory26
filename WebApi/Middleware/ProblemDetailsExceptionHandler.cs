using AppCore.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

// Piotr Bacior - WSEI Kraków

namespace WebApi.Middleware;

// Middleware obsługujący wyjątki i zamieniający je na ProblemDetails (RFC 7807).
// Implementuje IExceptionHandler — nowy mechanizm ASP.NET Core 8.
public class ProblemDetailsExceptionHandler(
    ProblemDetailsFactory factory, 
    ILogger<ProblemDetailsExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext context, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        // Obsługujemy tylko wyjątek ContactNotFoundException
        if (exception is ContactNotFoundException)
        {
            logger.LogInformation($"Exception handled: {exception.Message}");
            
            // Tworzymy obiekt ProblemDetails zgodny ze standardem
            var problem = factory.CreateProblemDetails(
                context,
                StatusCodes.Status404NotFound, // 404 — zasób nie istnieje
                "Contact service error!",
                detail: exception.Message
            );
            
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(problem, cancellationToken);
            return true; // wyjątek został obsłużony
        }

        // Jeśli to nie jest wyjątek, który obsługujemy — przepuszczamy dalej
        return false;
    }
}