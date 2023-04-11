using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Gateway.API.MessageBroker
{
    public class Producer// : IProducer
    {
        public void SendMessage<T>(T message)
        {
            //var factory = new ConnectionFactory { HostName = "localhost:15672/" };
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            //channel.QueueDeclare("orders");

            channel.QueueDeclare(queue: "orders",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            //channel.BasicPublish(exchange: "", routingKey: "orders", body: body);

            channel.BasicPublish(exchange: "",
                                     routingKey: "orders",
                                     basicProperties: null,
                                     body: body);
        }
    }
}
