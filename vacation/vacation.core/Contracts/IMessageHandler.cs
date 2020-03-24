namespace vacation.core.Contracts
{
    public interface IMessageHandler<in T>
    {
        void Handle(T message);
    }
}
