namespace SystemMonitor.Models.Configs;

public class KafkaConsumerSettings
{
    public string BootstrapServers { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public string AutoOffsetReset { get; set; } = "Latest"; 
}