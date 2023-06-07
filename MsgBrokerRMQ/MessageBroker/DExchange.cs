using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsgBrokerRMQ.MessageBroker
{
    public class DirectExchangePublicher
    {
        public static void Publish(IModel channel)
        {
            var res = "";
            channel.ExchangeDeclare("demo-direct-exchnage", ExchangeType.Direct);
            var count = 0;
            //while (true)
            while (count <= 5)
            {
                var msg = new { Name = "producer", Message = $"Hello rabbitMQ {count}", };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg));

                channel.BasicPublish("demo-direct-exchnage", "account.init", null, body);

                count++;
                //Thread.Sleep(1000);

                res = DirectExchangeConsumer.Consume(channel);

            }

            Console.WriteLine("\nThe result");
            Console.WriteLine("\n" + res);
        }
    }



    public class DirectExchangeConsumer
    {
        public static string Consume(IModel channel)
        {
            var message = "";
            var msg = "";

            channel.ExchangeDeclare("demo-direct-exchnage", ExchangeType.Direct);

            channel.QueueDeclare(queue: "demo-direct-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            channel.QueueBind("demo-direct-queue", "demo-direct-exchnage", "account.init");

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, eventArgs) =>
            {
                //var body = ea.Body;
                var body = eventArgs.Body.ToArray();

                message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" Message received: " + message);
                msg = message;
                //channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
                //Thread.Sleep(1000);
            };

            channel.BasicConsume(queue: "demo-direct-queue", autoAck: true, consumer: consumer);

            return msg;
        }
    }




    public class TopicExchangePublicher
    {
        public static void Publish(IModel channel)
        {
            var res = "";
            var ttl = new Dictionary<string, object>
            {
                {"x=message", 30000 }
            };
            channel.ExchangeDeclare(exchange: "demo-topic-exchnage", ExchangeType.Direct, arguments: ttl);
            var count = 0;
            //while (true)
            while (count <= 5)
            {
                var msg = new { Name = "producer", Message = $"Hello rabbitMQ {count}", };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(msg));

                channel.BasicPublish("demo-topic-exchnage", "account.init", null, body);

                count++;
                //Thread.Sleep(1000);

                res = DirectExchangeConsumer.Consume(channel);

            }

            Console.WriteLine("\nThe result");
            Console.WriteLine("\n" + res);
        }
    }



    public class TopicExchangeConsumer
    {
        public static string Consume(IModel channel)
        {
            var message = "";
            var msg = "";

            channel.ExchangeDeclare("demo-topic-exchnage", ExchangeType.Direct);

            channel.QueueDeclare(queue: "demo-topic-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            channel.QueueBind("demo-topic-queue", "demo-direct-exchnage", "account.*");

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, eventArgs) =>
            {
                //var body = ea.Body;
                var body = eventArgs.Body.ToArray();

                message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" Message received: " + message);
                msg = message;
                //channel.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
                //Thread.Sleep(1000);
            };

            channel.BasicConsume(queue: "demo-direct-queue", autoAck: true, consumer: consumer);

            return msg;
        }
    }
}
