using Microsoft.Extensions.DependencyInjection;
using UPBank.Customer.Domain.Commands.CreateCustomer;
using UPBank.Customer.Domain.Commands.DeleteCustomer;
using UPBank.Customer.Domain.Commands.UpdateCustomer;
using UPBank.Customer.Domain.Queries.GetAllCustomers;
using UPBank.Customer.Domain.Queries.GetCustomerByCPF;

namespace UPBank.Customer.Domain
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
            services
                .AddTransient<CreateCustomerCommandHandler>()
                .AddTransient<UpdateCustomerCommandHandler>()
                .AddTransient<DeleteCustomerCommandHandler>();
            return services;
        }

        private static IServiceCollection AddQueries(this IServiceCollection services)
        {
            return
            services
                .AddTransient<GetCustomerByCPFQueryHandler>()
                .AddTransient<GetAllCustomerQueryHandler>();
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            //services.AddScoped<IValidator<CreatePersonCommand>, CreatePersonCommandValidator>();
            return services;
        }
    }
}