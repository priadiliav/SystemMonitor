namespace SystemMonitor.MessageBroker;

public interface IKafkaConsumer<TKey, TValue> where TValue : class
{
    Task Consume(CancellationToken stoppingToken);
    void Close();
    void Dispose();
}