namespace Gateway.API.MessageBroker
{
    public interface IOroducer
    {
        void SendMessage<T>(T message);
    }
}
