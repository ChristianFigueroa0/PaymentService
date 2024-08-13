using Microsoft.Extensions.DependencyInjection;
using PaymentService.Domain.PaymentAggregate;
using PaymentService.Infrastructure.Repositories;

namespace PaymentService.Infrastructure
{
    /// <summary>
    /// Dependency injection to infrastructure layer.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Add all required service to use infrastructure layer.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <returns><see cref="IServiceCollection"/> with infrastructure services added to DI.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            return services;
        }

    }
}
