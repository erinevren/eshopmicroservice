using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error message: {exceptionMessage}, Time of occurence {time}", exception.Message, DateTime.UtcNow);

            (string Details, string Title, int StatusCode) = exception switch
            {
                BadRequestException => (exception.Message, exception.GetType().Name, StatusCodes.Status400BadRequest),

                NotFoundException  => (exception.Message, exception.GetType().Name, StatusCodes.Status404NotFound),

                InternalServerException  => (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError),

                ValidationException  => (exception.Message, exception.GetType().Name, StatusCodes.Status400BadRequest),

                _ => (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError)
            };

            var problemDetails = new ProblemDetails
            {
                Title = Title,
                Detail = Details,
                Status = StatusCode,
                Instance = context.TraceIdentifier,
            };

            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

            if(exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("details", validationException.Errors);
            }
            
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
