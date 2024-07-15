using System.Data;
using System.Data.SqlClient;
using UPBank.Customer.Application.Contracts;
using UPBank.Customer.Application.RabbitMQ;
using UPBank.Customer.Application.Services;
using UPBank.Customer.Domain.Contracts;
using UPBank.Customer.Infra.Context;
using UPBank.Customer.Infra.Repostories;
using UPBank.Utils.Address.Contracts;
using UPBank.Utils.Address.Services;
using UPBank.Utils.Person.Contracts;
using UPBank.Utils.Person.Services;

namespace UPBank.Customer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<IDbConnection>(db => new SqlConnection(builder.Configuration.GetConnectionString("UpBankApiCustomerContext")));

            // Add services to the container.
            builder.Services.AddSingleton<IUpBankApiCustomerContext, UpBankApiCustomerContext>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddSingleton<RabbitMQPublisher>();

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            var publisher = new RabbitMQPublisher();

            app.Run();
        }
    }
}
