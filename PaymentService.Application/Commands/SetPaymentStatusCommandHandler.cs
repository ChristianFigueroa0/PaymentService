using FluentValidation;
using MediatR;
using PaymentService.Domain.Enums;
using PaymentService.Domain.Exceptions;
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
    public class SetPaymentStatusCommandHandler(IPaymentRepository paymentRepository, IValidator<SetPaymentStatusCommand> validator) : IRequestHandler<SetPaymentStatusCommand, Unit>
    {
        /// <summary>
        /// Fluent validator to <see cref="SetPaymentStatusCommand"/>.
        /// </summary>
        private readonly IValidator<SetPaymentStatusCommand> _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        /// <summary>
        /// Payment repository.
        /// </summary>
        private readonly IPaymentRepository _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
        /// <summary>
        /// Handle command to set payment status.
        /// </summary>
        /// <param name="request">Set payment status command.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>True if status changed successful.</returns>
        public async Task<Unit> Handle(SetPaymentStatusCommand request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            if (!Enum.TryParse<PaymentStatus>(request.Status, true, out var paymentStatus))
                throw new ArgumentException("Invalid status value.");

            var success = await _paymentRepository.SetStatus(request.PaymentId, paymentStatus);

            if(!success)
                throw new NotFoundException("Payment", request.PaymentId);

            return Unit.Value;
        }
    }
}
