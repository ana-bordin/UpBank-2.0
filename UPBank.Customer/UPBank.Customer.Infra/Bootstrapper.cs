using Microsoft.Extensions.DependencyInjection;
using UPBank.Customer.Domain.Contracts.UPBank.Customer.Domain.Contracts;
using UPBank.Customer.Infra.Context;
using UPBank.Customer.Infra.Repostories;

namespace UPBank.Customer.Infra
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddInfraContext(this IServiceCollection services)
        {
            services
                .AddSingleton<IUpBankApiCustomerContext, UpBankApiCustomerContext>()
                .AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}