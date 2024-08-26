using System.Data;
using System.Data.SqlClient;
using UPBank.Customer.Domain;
using UPBank.Customer.Infra;
using UPBank.Utils.CrossCutting.Exception;
using UPBank.Utils.Integration.Person;

namespace UPBank.Customer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<IDbConnection>(db => new SqlConnection(builder.Configuration.GetConnectionString("UpBankApiCustomerContext")));

            // Add services to the container.

            builder.Services.AddDomainContext();
            builder.Services.AddInfraContext();
            builder.Services.AddCrossCuttingContext();
            builder.Services.AddIntegrationPersonContext();

            //builder.Services.AddSingleton<RabbitMQPublisher>();

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            //var publisher = new RabbitMQPublisher();

            app.Run();
        }
    }
}