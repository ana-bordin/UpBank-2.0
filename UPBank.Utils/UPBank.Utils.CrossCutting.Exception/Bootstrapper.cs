using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UPBank.Utils.CrossCutting.Exception.Contracts;
using UPBank.Utils.CrossCutting.Exception.Pipes;
using UPBank.Utils.CrossCutting.Exception.Services;

namespace UPBank.Utils.CrossCutting.Exception
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddCrossCuttingContext(this IServiceCollection services)
        {
            return services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastValidation<,>))
            .AddScoped<IDomainNotificationService, DomainNotificationServiceHandler>()
            .AddScoped<TryService>();
        }
    }
}