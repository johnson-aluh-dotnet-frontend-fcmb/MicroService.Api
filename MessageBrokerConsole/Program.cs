using Gateway.API.MessageBroker;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace MessageBrokerConsole
{
    class Program
    {
        private readonly Producer _messagePublisher = new Producer();
        //private readonly IProducer _messagePublisher;
        //public Program(IProducer messagePublisher)
        //{
        //    _messagePublisher = messagePublisher;
        //}
        static void Main(string[] args)
        {


            Console.WriteLine("Before caclling _messagePublisher.SendMessage(order)!");

            var obj = new { Name = "John" };

            new Program()._messagePublisher.SendMessage(obj);

            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            //channel.QueueDeclare("orders");
            channel.QueueDeclare(queue: "orders",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);


            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, eventArgs) =>
            {
                //var body = ea.Body;
                var body = eventArgs.Body.ToArray();

                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" Location received: " + message);
                channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
                Thread.Sleep(1000);
            };
            channel.BasicConsume(queue: "orders",
                                 autoAck: false,
                                 consumer: consumer);

            //consumer.Received += (model, eventArgs) =>
            //{
            //    var body = eventArgs.Body.ToArray();
            //    var message = Encoding.UTF8.GetString(body);

            //    Console.WriteLine(message);
            //};
            //channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);



            Console.ReadKey();
        
        
        }
    }
}
