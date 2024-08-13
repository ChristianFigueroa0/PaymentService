using PaymentService.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace PaymentService.Domain.PaymentAggregate
{
    /// <summary>
    /// Define the payment repository.
    /// </summary>
    public interface IPaymentRepository
    {
        /// <summary>
        /// Add a new payment.
        /// </summary>
        /// <param name="payment">Payment data.</param>
        /// <returns>The id of the created payment.</returns>
        Task<Guid> Add(Payment payment);
        /// <summary>
        /// Set payment status.
        /// </summary>
        /// <param name="id">Id of the payment to change status.</param>
        /// <param name="status">New status.</param>
        Task<bool> SetStatus(Guid id, PaymentStatus status);
    }
}
