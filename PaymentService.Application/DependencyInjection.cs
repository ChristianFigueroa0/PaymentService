using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentService.Application
{
    /// <summary>
    /// Dependency injection to application layer.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Add all required service to use application layer.
        /// </summary>
        /// <param name="services">DI container.</param>
        /// <returns><see cref="IServiceCollection"/> with application services added to DI.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);
            return services;
        }
    }
}
