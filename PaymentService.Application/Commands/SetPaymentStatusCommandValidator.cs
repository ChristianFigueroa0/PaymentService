using FluentValidation;
using PaymentService.Domain.Enums;
using System;

namespace PaymentService.Application.Commands
{
    /// <summary>
    /// Validator for the SetPaymentStatusCommand class.
    /// </summary>
    public sealed class SetPaymentStatusCommandValidator : AbstractValidator<SetPaymentStatusCommand>
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public SetPaymentStatusCommandValidator()
        {
            RuleFor(command => command.PaymentId)
            .NotEmpty()
            .WithMessage("PaymentId is required.");

            RuleFor(command => command.Status)
                .Must(BeAValidPaymentStatus)
                .WithMessage("Invalid status value. The status must be one of the following: Pending, Authorized, Processing, Completed, Failed, Declined, or Cancelled.");
        }
        /// <summary>
        /// Checks if the given status string is a valid value of the <see cref="PaymentStatus"/> enum.
        /// </summary>
        /// <param name="status">The status string to validate.</param>
        /// <returns>true if the status is valid; otherwise, false.</returns>
        private bool BeAValidPaymentStatus(string status)
            => Enum.TryParse(typeof(PaymentStatus), status, out _);
    }
}
