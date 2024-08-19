using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UPBank.Address.Domain.Commands.CreateAddress;
using UPBank.Address.Domain.Commands.DeleteAddress;
using UPBank.Address.Domain.Commands.UpdateAddress;
using UPBank.Address.Domain.Contracts;
using UPBank.Address.Domain.Queries.GetAddressById;
using UPBank.Address.Domain.Services;
using UPBank.Utils.CrossCutting.Exception.Contracts;
using UPBank.Utils.CrossCutting.Exception.Pipes;
using UPBank.Utils.CrossCutting.Exception.Services;

namespace UPBank.Address.Domain
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
            .AddScoped<IViaCepService, ViaCepService>()
            .AddCommands()
            .AddQueries();
        }

        private static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddTransient<CreateAddressCommandHandler>();
            services.AddTransient<UpdateAddressCommandHandler>();
            services.AddTransient<DeleteAddressCommandHandler>();
            return services;
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            services.AddTransient<GetAddressByIdQueryHandler>();
            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateAddressCommand>, CreateAddressCommandValidator>();
            return services;
        }
    }
}