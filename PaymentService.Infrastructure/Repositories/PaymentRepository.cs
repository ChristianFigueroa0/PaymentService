using Microsoft.Extensions.Configuration;
using PaymentService.Domain.Enums;
using PaymentService.Domain.PaymentAggregate;
using System;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PaymentService.Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IPaymentRepository"/> with dapper.
    /// </summary>
    /// <param name="configuration">Allows access to appsettings.json</param>
    public class PaymentRepository(IConfiguration configuration) : IPaymentRepository
    {
        /// <summary>
        /// Connection string of payments db.
        /// </summary>
        private readonly string _connectionString = configuration.GetConnectionString("PaymentsDb");
        /// <summary>
        /// Name of sp to add new payment.
        /// </summary>
        private const string NameSpAddPayment = "AddPayment";
        /// <summary>
        /// Name of sp to set payment status.
        /// </summary>
        private const string NameSpSetPaymentStatus = "SetPaymentStatus";
        /// <summary>
        /// Add a new payment.
        /// </summary>
        /// <param name="payment">Payment data.</param>
        /// <returns>The id of the created payment.</returns>
        public async Task<Guid> Add(Payment payment)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            return await sqlConnection.QueryFirstOrDefaultAsync<Guid>(NameSpAddPayment, new
            {
                payment.Description,
                payment.QtyProducts,
                payment.Amount,
                payment.Sender,
                payment.Receiver,
                Status = payment.Status.ToString()
            },
            commandType: CommandType.StoredProcedure);
        }
        /// <summary>
        /// Set payment status.
        /// </summary>
        /// <param name="id">Id of the payment to change status.</param>
        /// <param name="status">New status.</param>
        public async Task<bool> SetStatus(Guid id, PaymentStatus status)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            var rowAffected = await sqlConnection.ExecuteAsync(NameSpSetPaymentStatus,
                new { Id = id, Status = status.ToString() },
            commandType: CommandType.StoredProcedure);

            return rowAffected > 0;
        }
    }
}
