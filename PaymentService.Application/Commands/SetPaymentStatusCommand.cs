using MediatR;
using System;

namespace PaymentService.Application.Commands
{
    /// <summary>
    /// Set payment status command.
    /// </summary>
    public class SetPaymentStatusCommand : IRequest<Unit>
    {
        /// <summary>
        /// The id of the payment to change status.
        /// </summary>
        public Guid PaymentId { get; set; }
        /// <summary>
        /// New status of the payment.
        /// </summary>
        public string Status { get; set; }
    }
}
