using MediatR;
using UPBank.Person.Domain;
using UPBank.Person.Infra;
using UPBank.Utils.CrossCutting.Exception.Pipes;

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
            builder.Services.AddAutoMapper(typeof(Program));

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