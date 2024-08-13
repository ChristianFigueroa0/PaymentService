using MediatR;
using PaymentService.Application.Commands;
using PaymentService.Application.Queries;

namespace PaymentService.Api.Endpoints
{
    /// <summary>
    /// Payments endpoints extensor.
    /// </summary>
    public static class PaymentsEndpoint
    {
        /// <summary>
        /// Register all endpoints related to payments
        /// </summary>
        /// <param name="app">Web application</param>
        /// <returns>Web application with endpoints added</returns>
        public static WebApplication RegisterPaymentsEndpoints(this WebApplication app)
        {
            var paymentsGroup = app.MapGroup("/api/payments");

            paymentsGroup.MapGet("/{id:guid}/status", GetStatus);
            paymentsGroup.MapPost("/", AddPayment);
            paymentsGroup.MapPost("/setStatus", SetStatus);

            return app;
        }
        /// <summary>
        /// Add new payment.
        /// </summary>
        /// <param name="command">Payment data.</param>
        /// <param name="mediator">Injected mediator</param>
        /// <returns>Id of created payment.</returns>
        static async Task<IResult> AddPayment(AddPaymentCommand command, IMediator mediator) => Results.Ok(await mediator.Send(command));
        /// <summary>
        /// Set payment status.
        /// </summary>
        /// <param name="command">Set status data.</param>
        /// <param name="mediator">Injected mediator.</param>
        /// <returns>Empty response.</returns>
        static async Task<IResult> SetStatus(SetPaymentStatusCommand command, IMediator mediator) => Results.Ok(await mediator.Send(command));
        /// <summary>
        /// Get payment status.
        /// </summary>
        /// <param name="id">Id of payment.</param>
        /// <param name="mediator">Injected mediator.</param>
        /// <returns>Payment status.</returns>
        static async Task<IResult> GetStatus(Guid id, IMediator mediator)
        {
            var query = new GetPaymentStatusQuery() { PaymentId = id };
            var paymentStatus = await mediator.Send(query);
            return Results.Ok(paymentStatus);
        }

    }
}