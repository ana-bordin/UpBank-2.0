using Microsoft.Extensions.DependencyInjection;

namespace UPBank.Agency.Infra
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddInfraContext(this IServiceCollection services)
        {
            //    services
            //.AddSingleton<IUpBankApiAgencyContext, UpBankApiAgencyContext>()
            //.AddScoped<IAgencyRepository, AgencyRepository>();

            return services;
        }
    }
}