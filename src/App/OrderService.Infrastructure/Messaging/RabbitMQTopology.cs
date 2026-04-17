using RabbitMQ.Client;

namespace OrderService.Infrastructure.Messaging;

public static class RabbitMqTopology
{
    public static void ConfigureQueue(IModel channel, string queueName)
    {
        // Main queue
        var mainArgs = new Dictionary<string, object>
        {
            { "x-dead-letter-exchange", "" },
            { "x-dead-letter-routing-key", $"{queueName}.retry" }
        };

        channel.QueueDeclare(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: mainArgs
        );

        // Retry queue
        var retryArgs = new Dictionary<string, object>
        {
            { "x-dead-letter-exchange", "" },
            { "x-dead-letter-routing-key", queueName },
            { "x-message-ttl", 5000 }
        };

        channel.QueueDeclare(
            queue: $"{queueName}.retry",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: retryArgs
        );

        // DLQ
        channel.QueueDeclare(
            queue: $"{queueName}.dlq",
            durable: true,
            exclusive: false,
            autoDelete: false
        );
    }
}