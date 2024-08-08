using Microsoft.Data.SqlClient;
using System.Data;
using UPBank.Employee.Application.Contracts;
using UPBank.Employee.Domain.Contracts;
using UPBank.Employee.Infra.Context;
using UPBank.Employee.Infra.Repositories;
using UPBank.Utils.Address.Contracts;
using UPBank.Utils.Address.Services;

namespace UPBank.Employee.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<IDbConnection>(db => new SqlConnection(builder.Configuration.GetConnectionString("UpBankApiEmployeeContext")));

            // Add services to the container.
            builder.Services.AddSingleton<IUpBankApiEmployeeContext, UpBankApiEmployeeContext>();
            builder.Services.AddSingleton<RabbitMQConsumer>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            var consumer = app.Services.GetService<RabbitMQConsumer>();
            consumer.StartListening();

            app.Run();
        }
    }
}
