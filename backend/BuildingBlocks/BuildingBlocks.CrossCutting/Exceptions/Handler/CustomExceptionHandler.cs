using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.CrossCutting.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Error Message: {exceptionMessage}, Time of occurrence {time}",
            exception.Message, DateTime.UtcNow);

        (string detail, string title, int statusCode) details = exception switch
        {
            InternalServerException => (exception.Message , exception.GetType().Name, StatusCodes.Status500InternalServerError),

            NotFoundException notFoundException => (notFoundException.Message, notFoundException.GetType().Name, StatusCodes.Status404NotFound),

            BadRequestException badRequestException => (badRequestException.Message, badRequestException.GetType().Name, StatusCodes.Status400BadRequest),

            ValidationException validationException => (validationException.Message, validationException.GetType().Name, StatusCodes.Status400BadRequest),
            
            _ => ("An unexpected error occurred.", "Internal Server Error", StatusCodes.Status500InternalServerError)
        };

        var problemDetails = new ProblemDetails
        {
            Title = details.title,
            Detail = details.detail,
            Status = details.statusCode,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        if(exception is ValidationException validationEx)
        {
            problemDetails.Extensions.Add("errors", validationEx.Errors);
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
