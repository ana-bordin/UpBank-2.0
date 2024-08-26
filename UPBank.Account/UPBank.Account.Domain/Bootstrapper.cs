using Microsoft.Extensions.DependencyInjection;

namespace UPBank.Account.Domain
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddDomainContext(this IServiceCollection services)
        {
            return services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Bootstrapper)))
                //.AddValidators()
                .AddAutoMapper(typeof(Bootstrapper))
                //.AddCommands()
                //.AddServices()
                //.AddQueries()
                ;
        }

        //private static IServiceCollection AddValidators(this IServiceCollection services)
        //{
        //    return
        //       services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();
        //}

        //private static IServiceCollection AddCommands(this IServiceCollection services)
        //{
        //    return services
        //        .AddTransient<CreatePersonCommandHandler>()
        //        .AddTransient<UpdatePersonCommandHandler>();
        //}

        //private static IServiceCollection AddServices(this IServiceCollection services)
        //{
        //    return services
        //        .AddTransient<IAddressService, AddressService>();
        //}

        //private static IServiceCollection AddQueries(this IServiceCollection services)
        //{
        //    return
        //        services.AddTransient<GetPersonByCPFQueryHandler>();
        //}
    }
}
