using Microsoft.Data.SqlClient;
using System.Data;
using UPBank.Person.Application.Contracts;
using UPBank.Person.Application.Services;
using UPBank.Person.Domain.Contracts;
using UPBank.Person.Infra.Context;
using UPBank.Person.Infra.Repositories;
using UPBank.Utils.Address.Contracts;
using UPBank.Utils.Address.Services;

namespace UPBank.Person.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSingleton<IDbConnection>(db => new SqlConnection(builder.Configuration.GetConnectionString("UpBankApiPersonContext")));

            // Add services to the container.
            builder.Services.AddSingleton<IUpBankApiPersonContext, UpBankApiPersonContext>();
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();
            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IAddressService, AddressService>();

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
