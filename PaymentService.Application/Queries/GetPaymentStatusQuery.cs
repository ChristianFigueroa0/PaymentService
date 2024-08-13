using MediatR;
using System;

namespace PaymentService.Application.Queries
{
    /// <summary>
    /// Query to get status of payment.
    /// </summary>
    public class GetPaymentStatusQuery : IRequest<GetPaymentStatusQueryResponse>
    {
        /// <summary>
        /// The id of payment to get status.
        /// </summary>
        public Guid PaymentId { get; set; }
    }
}
