using System.Data;
using System.Data.SqlClient;
using UPBank.Address.Domain.Contracts;
using UPBank.Address.Domain.Services;
using UPBank.Address.Infra.Context;
using UPBank.Address.Infra.Repositories;

namespace UPBankAPI.Address
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
