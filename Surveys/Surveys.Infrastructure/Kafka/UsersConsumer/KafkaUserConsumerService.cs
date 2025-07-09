using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Surveys.Infrastructure.Kafka.UsersConsumer;

public class KafkaUserConsumerService : BackgroundService
{
    private readonly KafkaUserConsumerRouter _userConsumerRouter;
    private readonly ILogger<KafkaUserConsumerService> _logger;
    private readonly IConsumer<Null, string> _consumer;
    private const string Topic = "user-events";

    public KafkaUserConsumerService(
        KafkaUserConsumerRouter userConsumerRouter, ILogger<KafkaUserConsumerService> logger)
    {
        _userConsumerRouter = userConsumerRouter;
        
        var config = new ConsumerConfig
        {
            BootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVER"),
            GroupId = "user-consumer",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<Null, string>(config)
            .Build();

        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(Topic);

        while (true)
        {
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);

                await _userConsumerRouter.RouteUserEventAsync(consumeResult.Message);
            }
            catch (ConsumeException e)
            {
                _logger.LogError(e.Error.Reason);
            }
        }
    }
    
    
}