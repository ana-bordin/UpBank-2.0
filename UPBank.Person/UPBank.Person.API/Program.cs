using UPBank.Person.Domain;
using UPBank.Person.Infra;
using UPBank.Utils.CrossCutting.Exception;
using UPBank.Utils.Integration.Address;

namespace UPBank.Person.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDomainContext();
            builder.Services.AddInfraContext();
            builder.Services.AddIntegrationAddressContext();
            builder.Services.AddCrossCuttingContext();

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}