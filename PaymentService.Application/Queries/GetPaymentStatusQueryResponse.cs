using System;

namespace PaymentService.Application.Queries
{
    /// <summary>
    /// Response of query to get payment status.
    /// </summary>
    public class GetPaymentStatusQueryResponse
    {
        /// <summary>
        /// Payment id.
        /// </summary>
        public Guid PaymentId { get; set; }
        /// <summary>
        /// Payment status.
        /// </summary>
        public string Status { get; set; }
    }
}
