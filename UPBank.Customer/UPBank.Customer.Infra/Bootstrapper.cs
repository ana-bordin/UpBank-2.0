using Microsoft.Extensions.DependencyInjection;
using UPBank.Customer.Domain.Contracts.UPBank.Customer.Domain.Contracts;
using UPBank.Customer.Infra.Data.Context.Customer;
using UPBank.Customer.Infra.Data.Repostories;

namespace UPBank.Customer.Infra
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddInfraContext(this IServiceCollection services)
        {
            services
                .AddSingleton<IUpBankApiCustomerContext, UpBankApiPersonContext>()
                .AddScoped<ICustomerRepository, CustomerRepository>();

            return services;
        }
    }
}