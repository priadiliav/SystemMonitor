using System.Text;
using System.Text.Json;
using Confluent.Kafka;

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