namespace Gateway.API.MessageBroker
{
    // public interface IMessageProducer
    public interface IProducer
    {
        void SendMessage<T>(T message);
    }
}
