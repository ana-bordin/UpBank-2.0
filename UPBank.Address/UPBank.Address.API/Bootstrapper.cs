namespace UPBank.Address.API
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddAPIContext(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(typeof(Bootstrapper));
        }
    }
}