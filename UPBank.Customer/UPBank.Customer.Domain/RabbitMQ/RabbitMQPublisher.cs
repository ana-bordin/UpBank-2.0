using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace UPBank.Customer.Domain.RabbitMQ
{
    public class RabbitMQPublisher
    {
        //private readonly EmployeeService _employeeService
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQPublisher(/*employee service*/)
        {
            //_employeeService = employeeService;
            _factory = new ConnectionFactory { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "customer", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void Publish(List<Entities.Customer> customer)
        {
            var message = JsonConvert.SerializeObject(customer);
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: "customer", basicProperties: null, body: body);
        }
    }
}
