using MediatR;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System;
using PaymentService.Domain.Exceptions;

namespace PaymentService.Application.Queries
{
    /// <summary>
    /// Get payment status query.
    /// </summary>
    /// <param name="configuration">Allows access to appsettings.json.</param>
    public class GetPaymentStatusQueryHandler(IConfiguration configuration) : IRequestHandler<GetPaymentStatusQuery, GetPaymentStatusQueryResponse>
    {
        /// <summary>
        /// Connection string of payments db.
        /// </summary>
        private readonly string _connectionString = configuration.GetConnectionString("PaymentsDb");
        /// <summary>
        /// Name of sp to get payment status.
        /// </summary>
        private const string NameSpGetPaymentStatus = "GetPaymentStatus";
        /// <summary>
        /// Handle query to get payment status.
        /// </summary>
        /// <param name="request">Payment id.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/>.</param>
        /// <returns>Payment status data.</returns>
        public async Task<GetPaymentStatusQueryResponse> Handle(GetPaymentStatusQuery request, CancellationToken cancellationToken)
        {
            using var sqlConnection = new SqlConnection(_connectionString);
            var result = await sqlConnection.QueryFirstOrDefaultAsync<GetPaymentStatusQueryResponse>(NameSpGetPaymentStatus,
                new { Id = request.PaymentId },
            commandType: CommandType.StoredProcedure);

            if(result is null)
                throw new NotFoundException("Payment", request.PaymentId);

            return result;
        }
    }
}
