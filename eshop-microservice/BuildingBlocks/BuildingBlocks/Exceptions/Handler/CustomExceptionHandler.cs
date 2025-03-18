using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
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
                BadRequestException badRequestException => (exception.Message, exception.GetType().Name, StatusCodes.Status400BadRequest),
                NotFoundException notFoundException => (exception.Message, exception.GetType().Name, StatusCodes.Status404NotFound),
                InternalServerException internalServerException => (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError),
                _ => (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError)
            };
        }
    }
}
