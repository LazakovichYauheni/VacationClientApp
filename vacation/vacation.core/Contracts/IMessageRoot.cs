namespace vacation.core.Contracts
{
    public interface IMessageRoot
    {
        void Subscribe<T>(IMessageHandler<T> messageHandler);

        void Unsubscribe<T>(IMessageHandler<T> messageHandler);

        void Raise<T>(T evnt);
    }
}
