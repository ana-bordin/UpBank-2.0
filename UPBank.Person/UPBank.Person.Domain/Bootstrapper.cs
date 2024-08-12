using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UPBank.Person.Domain.Commands.CreatePerson;
using UPBank.Person.Domain.Commands.UpdatePerson;
using UPBank.Utils.Address.Contracts;
using UPBank.Utils.Address.Services;
using UPBank.Utils.CommonsFiles.Contracts;
using UPBank.Utils.CommonsFiles.Pipes;
using UPBank.Utils.CommonsFiles.Services;

namespace UPBank.Person.Domain
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddDomainContext(this IServiceCollection services)
        {
            return services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Bootstrapper)))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastValidation<,>))
                .AddValidators()
                .AddAutoMapper(typeof(Bootstrapper))
                .AddScoped<IDomainNotificationService, DomainNotificationServiceHandler>()
                .AddCommands()
                .AddServices();
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
             services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();
        }

        private static IServiceCollection AddCommands(this IServiceCollection services)
        {
            return services
                .AddTransient<CreatePersonCommandHandler>()
                .AddTransient<UpdatePersonCommandHandler>();
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IAddressService, AddressService>();
        }
    }
}