namespace SystemMonitor.MessageBroker;

public interface IKafkaHandler<in TKey, in TValue> where TValue : class
{
    Task HandleAsync(TKey key, TValue value);
}