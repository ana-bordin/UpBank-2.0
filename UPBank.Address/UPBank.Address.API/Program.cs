using System.Data;
using System.Data.SqlClient;
using UPBank.Address.Application.Contracts;
using UPBank.Address.Application.Services;
using UPBank.Address.Infra.Context;
using UPBank.Address.Infra.Repositories;

namespace UPBank.Address.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddSingleton<IDbConnection>(db => new SqlConnection(builder.Configuration.GetConnectionString("UpBankApiAddressContext")));

            // Add services to the container.
            builder.Services.AddSingleton<IUpBankApiAddressContext, UpBankApiAddressContext>();

            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<IViaCepService, ViaCepService>();

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
