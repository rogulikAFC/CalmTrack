using Microsoft.Extensions.DependencyInjection;

namespace Mail.Infrastructure.Kafka.UserConsumer;

public static class AddKafkaUserConsumerExtension
{
    public static void AddKafkaUserConsumer(
        this IServiceCollection services)
    {
        services.AddSingleton<KafkaUserConsumerRouter>();
        services.AddHostedService<KafkaUserConsumerService>();
    }
}