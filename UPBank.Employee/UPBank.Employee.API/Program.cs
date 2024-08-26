using UPBank.Employee.Domain;
using UPBank.Employee.Infra;
using UPBank.Utils.CrossCutting.Exception;
using UPBank.Utils.Integration.Person;

namespace UPBank.Employee.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDomainContext();
            builder.Services.AddInfraContext();
            builder.Services.AddCrossCuttingContext();
            builder.Services.AddIntegrationPersonContext();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            //var consumer = app.Services.GetService<RabbitMQConsumer>();
            //consumer.StartListening();

            app.Run();
        }
    }
}
