using Microsoft.Extensions.DependencyInjection;
using UPBank.Utils.Integration.Address.Contracts;
using UPBank.Utils.Integration.Address.Services;

namespace UPBank.Utils.Integration.Address
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddIntegrationAddressContext(this IServiceCollection services)
        {
            return services
                .AddTransient<IAddressService, AddressService>();
        }
    }
}
