using Microsoft.Extensions.DependencyInjection;

namespace UPBank.Agency.Domain
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddDomainContext(this IServiceCollection services)
        {
            return services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Bootstrapper)))
            .AddValidators()
            .AddAutoMapper(typeof(Bootstrapper))
            .AddCommands()
            .AddQueries();
        }

        private static IServiceCollection AddCommands(this IServiceCollection services)
        {
            //services.AddTransient<CreateAgencyCommandHandler>();
            //services.AddTransient<UpdateCustomerCommandHandler>();
            //services.AddTransient<DeleteCustomerCommandHandler>();
            return services;
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            //services.AddTransient<GetCustomerByCPFQueryHandler>();
            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            //services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();
            return services;
        }
    }
}