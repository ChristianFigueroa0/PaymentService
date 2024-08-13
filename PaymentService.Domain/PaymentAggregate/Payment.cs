using PaymentService.Domain.Enums;
using System;

namespace PaymentService.Domain.PaymentAggregate
{
    /// <summary>
    /// Payment entity.
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Id.
        /// </summary>
        public Guid Id { get; private set; } = Guid.NewGuid();
        /// <summary>
        /// Payment description.
        /// </summary>
        public string Description { get; private set; } = string.Empty;
        /// <summary>
        /// Quantity of products.
        /// </summary>
        public int QtyProducts { get; private set; }
        /// <summary>
        /// Sender.
        /// </summary>
        public string Sender { get; private set; } = string.Empty;
        /// <summary>
        /// Receiver.
        /// </summary>
        public string Receiver { get; private set; } = string.Empty;
        /// <summary>
        /// Status of payment.
        /// </summary>
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        /// <summary>
        /// Payment amount.
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="description">Description of payment.</param>
        /// <param name="qtyProducts">Quantity of products.</param>
        /// <param name="sender">Name of the sender</param>
        /// <param name="receiver">Name of the receiver</param>
        /// <param name="amount">Amount of the payment.</param>
        /// <param name="status">Status of payment.</param>
        public Payment(string description, int qtyProducts, string sender, string receiver, decimal amount, PaymentStatus status)
        {
            Description = description;
            QtyProducts = qtyProducts;
            Sender = sender;
            Receiver = receiver;
            Amount = amount;
            Status = status;
        }
    }
}
