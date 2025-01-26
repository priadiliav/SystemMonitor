using Confluent.Kafka;
using SystemMonitor.MessageBroker.Transformations;

namespace SystemMonitor.MessageBroker.Kafka;

public class KafkaProducer<TKey, TValue>(ProducerConfig config) : IDisposable, IProducer<TKey, TValue>
    where TValue : class
{
    private readonly Confluent.Kafka.IProducer<TKey, TValue> _producer = new ProducerBuilder<TKey, TValue>(config)
        .SetValueSerializer(new DefaultSerializer<TValue>()).Build();

    public async Task ProduceAsync(string topic, TKey key, TValue value)  
    {  
        await _producer.ProduceAsync(topic, new Message<TKey, TValue> { Key = key, Value = value });  
    }  
  
    public void Dispose()  
    {  
        _producer.Flush();  
        _producer.Dispose();  
    }  
}