using Microsoft.Extensions.DependencyInjection;
using UPBank.Utils.Integration.Person.Contracts;
using UPBank.Utils.Person.Services;

namespace UPBank.Utils.Integration.Person
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddIntegrationPersonContext(this IServiceCollection services)
        {
            return services
                .AddTransient<IPersonService, PersonService>();
        }
    }
}