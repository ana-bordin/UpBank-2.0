using Microsoft.Extensions.DependencyInjection;
using UPBank.Address.Domain.Contracts;
using UPBank.Address.Infra.Context;
using UPBank.Address.Infra.Repositories;
using UPBank.Utils.CommonsFiles.Contracts.Repositories;

namespace UPBank.Address.Infra
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddInfraContext(this IServiceCollection services)
        {
            return services
                .AddSingleton<IUpBankApiAddressContext, UpBankApiAddressContext>()
                .AddScoped<IAddressRepository, AddressRepository>()
                .AddScoped<IRepository<Domain.Entities.CompleteAddress>, CompleteAddressRepository>();
        }
    }
}