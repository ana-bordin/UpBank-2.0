using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UPBank.Person.Domain.Commands.CreatePerson;
using UPBank.Utils.Address.Contracts;
using UPBank.Utils.Address.Services;
using UPBank.Utils.CommonsFiles.Contracts;
using UPBank.Utils.CommonsFiles.Services;

namespace UPBank.Person.Domain
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddDomainContext(this IServiceCollection services)
        {
            return services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Bootstrapper)))
                //.AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastValidation<,>))
                .AddValidators()
                .AddAutoMapper(typeof(Bootstrapper))
                .AddScoped<IDomainNotificationService, DomainNotificationServiceHandler>()
                .AddCommands()
                .AddQueries()
                .AddServices();
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonCommandValidation>();
            return services;
        }

        private static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddTransient<CreatePersonCommandHandler>();
            return services;
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IAddressService, AddressService>();
        }
    }
}