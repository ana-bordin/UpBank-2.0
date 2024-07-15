using UPBank.Employee.Application.RabbitMQ;

namespace UPBank.Employee.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<RabbitMQConsumer>();
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
