using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using SystemMonitor.MessageBroker.Transformations;

namespace SystemMonitor.MessageBroker.Kafka;

public class KafkaConsumer<TKey, TValue>(
    ConsumerConfig config,
    IServiceScopeFactory serviceScopeFactory,
    IConsumer<TKey, TValue> consumer,
    string topic)
    : IKafkaConsumer<TKey, TValue>
    where TValue : class
{
    private IConsumer<TKey, TValue> _consumer = consumer;

    public async Task Consume(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        
        var handler = scope.ServiceProvider.GetRequiredService<IKafkaHandler<TKey, TValue>>();
        
        _consumer = new ConsumerBuilder<TKey, TValue>(config)
            .SetValueDeserializer(new DefaultDeserializer<TValue>()).Build();

        await Task.Run(() => StartConsumerLoop(handler, stoppingToken), stoppingToken);
    }

    private async Task StartConsumerLoop(IKafkaHandler<TKey, TValue> handler, CancellationToken cancellationToken)
    {
        _consumer.Subscribe(topic);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(cancellationToken);

                if (result != null)
                {
                    await handler.HandleAsync(result.Message.Key, result.Message.Value);
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (ConsumeException e)
            {
                if (e.Error.IsFatal)
                {
                    break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e}");
                break;
            }
        }
    }

    public void Close()
    {
        _consumer.Close();
    }

    public void Dispose()
    {
        _consumer.Dispose();
    }
}  