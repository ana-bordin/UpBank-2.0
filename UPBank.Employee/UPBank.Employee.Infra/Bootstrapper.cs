using Microsoft.Extensions.DependencyInjection;
using UPBank.Employee.Domain.Contracts;
using UPBank.Employee.Infra.Context;
using UPBank.Employee.Infra.Repositories;

namespace UPBank.Employee.Infra
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddInfraContext(this IServiceCollection services)
        {
            services
                .AddSingleton<IUpBankApiEmployeeContext, UpBankApiEmployeeContext>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }
    }
}
