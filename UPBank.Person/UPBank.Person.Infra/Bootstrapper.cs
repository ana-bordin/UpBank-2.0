using Microsoft.Extensions.DependencyInjection;
using UPBank.Person.Domain.Contracts;
using UPBank.Person.Infra.Context;
using UPBank.Person.Infra.Repositories;

namespace UPBank.Person.Infra
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddInfraContext(this IServiceCollection services)
        {
            return services
                .AddSingleton<IUpBankApiPersonContext, UpBankApiPersonContext>()
                .AddScoped<IPersonRepository, PersonRepository>()
                .AddAutoMapper(typeof(Bootstrapper))
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Bootstrapper)));
        }
    }
}