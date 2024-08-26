using Microsoft.Extensions.DependencyInjection;
using UPBank.Account.Domain.Contracts;
using UPBank.Account.Infra.Context;
using UPBank.Account.Infra.Repositories;

namespace UPBank.Account.Infra
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddInfraContext(this IServiceCollection services)
        {
            return services
                .AddSingleton<IUpBankApiAccountContext, UpBankApiAccountContext>()
                .AddScoped<IAccountRepository, AccountRepository>()
                .AddAutoMapper(typeof(Bootstrapper))
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Bootstrapper)));
        }
    }
}