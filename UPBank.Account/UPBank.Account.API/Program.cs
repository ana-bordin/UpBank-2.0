
using UPBank.Account.Infra;
using UPBank.Account.Domain;
using UPBank.Utils.CrossCutting.Exception;

namespace UPBank.Account.API
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