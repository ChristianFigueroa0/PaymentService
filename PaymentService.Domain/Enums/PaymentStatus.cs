namespace PaymentService.Domain.Enums
{
    /// <summary>
    /// All available payments status.
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// Pending payment.
        /// </summary>
        Pending,
        /// <summary>
        /// Authorized payment.
        /// </summary>
        Authorized,
        /// <summary>
        /// Processing payment
        /// </summary>
        Processing,
        /// <summary>
        /// Completed payment.
        /// </summary>
        Completed,
        /// <summary>
        /// Failed payment.
        /// </summary>
        Failed,
        /// <summary>
        /// Declined payment.
        /// </summary>
        Declined,
        /// <summary>
        /// Cancelled payment.
        /// </summary>
        Cancelled
    }
}
