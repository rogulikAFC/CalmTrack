using Microsoft.Extensions.DependencyInjection;

namespace Surveys.Infrastructure.Kafka.UsersConsumer;

public static class AddKafkaUserConsumerExtension
{
    public static IServiceCollection AddKafkaUserConsumer(
        this IServiceCollection services)
    {
        services.AddSingleton<KafkaUserConsumerRouter>();
        services.AddHostedService<KafkaUserConsumerService>();
        
        return services;
    }
}