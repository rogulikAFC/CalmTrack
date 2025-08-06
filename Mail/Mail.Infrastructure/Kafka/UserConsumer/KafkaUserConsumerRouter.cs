using Confluent.Kafka;
using Mail.Application.MailSending;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UserMessages;

namespace Mail.Infrastructure.Kafka.UserConsumer;

public class KafkaUserConsumerRouter(
    IMailSendingService mailSendingService, ILogger<KafkaUserConsumerRouter> logger)
{

    private static string ExtractEventType(Message<Null, string> message)
    {
        var header = message.Headers.GetLastBytes("event-type");
        
        return System.Text.Encoding.UTF8.GetString(header);
    }
    
    public async Task RouteUserEventAsync(Message<Null, string> message)
    {
        var eventType = ExtractEventType(message);

        switch (eventType)
        {
            case "UserCreated":
                var createUserMessage = JsonConvert
                    .DeserializeObject<CreateUserMessage>(message.Value)!;

                try
                {
                    await mailSendingService
                        .SendUserCreatedMailAsync(createUserMessage);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
                
                break;
            
            case "UserDeleted":
                var userId = new Guid(message.Value);

                try
                {
                    await mailSendingService
                        .SendUserDeletedMailAsync(userId);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
                
                break;
        }
    }
}