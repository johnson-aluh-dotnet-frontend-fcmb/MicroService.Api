namespace Gateway.API.MessageBroker
{
    public interface IOroducer
    {
        void ProduceMessage<T>(T message);
    }
}
