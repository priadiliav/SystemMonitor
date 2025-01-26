using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace SystemMonitor.MessageBroker.Transformations;

public class DefaultDeserializer<T> : IDeserializer<T>
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        if (isNull)
            return default;

        var json = Encoding.UTF8.GetString(data);
        
        return JsonSerializer.Deserialize<T>(json);
    }
}