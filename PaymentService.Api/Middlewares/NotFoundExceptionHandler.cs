using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Domain.Exceptions;

namespace PaymentService.Api.Middlewares
{
    /// <summary>
    /// Handles not found exception
    /// </summary>
    /// <param name="logger">Logger</param>
    internal sealed class NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger) : IExceptionHandler
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<NotFoundExceptionHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        /// <inheritdoc/>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not NotFoundException notFoundException)
                return false;

            _logger.LogError(notFoundException, "Exception occurred: {0}", notFoundException.Message);


            var problemDetails = new ProblemDetails
            {
                Title = "Entity not found",
                Detail = exception.Message,
                Status = StatusCodes.Status404NotFound
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
