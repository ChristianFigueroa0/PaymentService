using FluentValidation;
using PaymentService.Domain.Enums;
using System;

namespace PaymentService.Application.Commands
{
    /// <summary>
    /// Validator for the AddPaymentCommand class.
    /// </summary>
    public sealed class AddPaymentCommandValidator : AbstractValidator<AddPaymentCommand>
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        public AddPaymentCommandValidator()
        {
            RuleFor(payment => payment.Description)
            .NotEmpty()
            .WithMessage("Description is required.");

            RuleFor(payment => payment.QtyProducts)
                .GreaterThanOrEqualTo(1).WithMessage("Quantity of products must be at least 1.");

            RuleFor(payment => payment.Sender)
                .NotEmpty().WithMessage("Sender is required.")
                .MaximumLength(100).WithMessage("Sender must not exceed 100 characters.");

            RuleFor(payment => payment.Receiver)
                .NotEmpty()
                .WithMessage("Receiver is required.");

            RuleFor(payment => payment.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than zero.");

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
