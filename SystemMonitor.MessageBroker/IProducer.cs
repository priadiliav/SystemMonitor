namespace SystemMonitor.MessageBroker;

public interface IProducer<in TKey, in TValue> where TValue : class
{
    Task ProduceAsync(string topic, TKey key, TValue value);
}