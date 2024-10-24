using FluentValidation;
using UPBank.Person.Domain.Commands.CreatePerson;
using UPBank.Person.Domain.Commands.UpdatePerson;
using UPBank.Person.Domain.Contracts.Repositories;
using UPBank.Person.Domain.Contracts.Services;
using UPBank.Person.Domain.Queries.GetPersonByCPF;
using UPBank.Person.Infra.Data.Context;
using UPBank.Person.Infra.Data.Repositories;
using UPBank.Person.Infra.Services.ServiceHandlers;

namespace UPBank.Person.API
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
                .AddServices()
                .AddQueries();
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return
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
                .AddTransient<IAddressServiceClient, AddressServiceClient>();
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            return
                services.AddTransient<GetPersonByCPFQueryHandler>();
        }
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