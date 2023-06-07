using Gateway.API.MessageBroker;
using MsgBrokerRMQ.MessageBroker;
using RabbitMQ.Client;
using System;

namespace MessageBrokerConsole
{
    class Program
    {
        private readonly Producer _messagePublisher = new Producer();
        private readonly Consumer _messageConsumer = new Consumer();

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                //Uri = new Uri("amqp://guest:guest@localhost:15672")
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            Console.WriteLine("Before caclling _messagePublisher.SendMessage(order)!");

            var obj = new { Name = "John" };

            new Program()._messagePublisher.ProduceMessage(obj);

            new Program()._messageConsumer.ConsumeMessage();


            DirectExchangePublicher.Publish(channel);
            //DirectExchangeConsumer.Consume(channel);

            Console.ReadKey();

        }
    }
}
