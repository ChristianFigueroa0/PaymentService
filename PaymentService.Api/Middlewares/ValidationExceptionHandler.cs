using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Api.Middlewares
{
    /// <summary>
    /// Handles validation exception.
    /// </summary>
    /// <param name="logger">Logger.</param>
    internal sealed class ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger) : IExceptionHandler
    {
        /// <summary>
        /// Logger.
        /// </summary>
        private readonly ILogger<ValidationExceptionHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        /// <inheritdoc/>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ValidationException notFoundException)
                return false;

            _logger.LogError(notFoundException, "Exception occurred: {0}", notFoundException.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation error",
                Detail = "One or more validation errors has occurred"
            };

            var validationEx = exception as ValidationException;

            if (validationEx.Errors is not null)
            {
                problemDetails.Extensions["errors"] = validationEx.Errors
                    .Select(e => new {e.PropertyName, e.ErrorMessage});
            }

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
