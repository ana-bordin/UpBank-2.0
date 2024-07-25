using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using UPBank.Customer.Application.Models;

namespace UPBank.Customer.Application.RabbitMQ
{
    public class RabbitMQPublisher
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQPublisher()
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "customer", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void Publish(List<CustomerOutputModel> customers)
        {
            var message = JsonSerializer.Serialize(customers);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "", routingKey: "customer", basicProperties: null, body: body);
        }

        public void Close()
        {
            _connection.Close();
        }
    }
}
