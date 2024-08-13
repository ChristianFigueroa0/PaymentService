using FluentValidation;
using MediatR;
using PaymentService.Domain.Enums;
using PaymentService.Domain.PaymentAggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PaymentService.Application.Commands
{
    /// <summary>
    /// Handle add payment command.
    /// </summary>
    /// <param name="paymentRepository">Payment repository.</param>
    /// <param name="validator">Injected validator.</param>
    public class AddPaymentCommandHandler(IPaymentRepository paymentRepository, IValidator<AddPaymentCommand> validator) : IRequestHandler<AddPaymentCommand, Guid>
    {
        /// <summary>
        /// Fluent validator to <see cref="AddPaymentCommand"/>.
        /// </summary>
        private readonly IValidator<AddPaymentCommand> _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        /// <summary>
        /// Payment repository.
        /// </summary>
        private readonly IPaymentRepository _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        /// <summary>
        /// Handle command to add new payment.
        /// </summary>
        /// <param name="request">Payment command.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Identifier of created payment.</returns>
        public async Task<Guid> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            if (!Enum.TryParse<PaymentStatus>(request.Status, true, out var paymentStatus))
                throw new ArgumentException("Invalid status value.");

            var payment = new Payment(request.Description, request.QtyProducts, request.Sender, request.Receiver, request.Amount, paymentStatus);

            var createdPaymentId = await _paymentRepository.Add(payment);

            return createdPaymentId;
        }
    }
}
