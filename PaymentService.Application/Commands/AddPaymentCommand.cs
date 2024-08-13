using MediatR;
using System;

namespace PaymentService.Application.Commands
{
    /// <summary>
    /// Add payment command.
    /// </summary>
    public class AddPaymentCommand : IRequest<Guid>
    {
        /// <summary>
        /// Payment description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Quantity of products.
        /// </summary>
        public int QtyProducts { get; set; }
        /// <summary>
        /// Payment sender.
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// Payment receiver.
        /// </summary>
        public string Receiver { get; set; }
        /// <summary>
        /// Amount of payment.
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Payment status.
        /// </summary>
        public string Status { get; set; }
    }
}
