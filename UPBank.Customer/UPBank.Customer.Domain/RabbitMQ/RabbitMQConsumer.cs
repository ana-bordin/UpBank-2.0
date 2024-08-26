using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPBank.Customer.Domain.RabbitMQ
{
    public class RabbitMQConsumer
    {
        /*private readonly EmployeeService _employeeService*/
        //private readonly ConnectionFactory _factory;
        //private readonly IConnection _connection;
        //private readonly IModel _channel;

        //public RabbitMQConsumer(/*employee service*/)
        //{
        //    //_employeeService = employeeService;
        //    _factory = new ConnectionFactory { HostName = "localhost" };
        //    _connection = _factory.CreateConnection();
        //    _channel = _connection.CreateModel();
        //    _channel.QueueDeclare(queue: "customer", durable: false, exclusive: false, autoDelete: false, arguments: null);
        //}

        //public void StartListening()
        //{
        //    var consumer = new EventingBasicConsumer(_channel);
        //    consumer.Received += (model, ea) =>
        //    {
        //        var body = ea.Body.ToArray();
        //        var message = Encoding.UTF8.GetString(body);
        //        Console.WriteLine(" [x] Received {0}", message);
        //        Console.WriteLine("Done");
        //        //var customer = JsonConvert.DeserializeObject<Customer>(message);
        //        //_employeeService.AddEmployee(employee);
        //    };
        //    _channel.BasicConsume(queue: "customer", autoAck: true, consumer: consumer);
        //}

    }
}