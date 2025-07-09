using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Surveys.Application.Features.Exceptions;
using Surveys.Application.Features.Users.Commands.CreateUser;
using Surveys.Application.Features.Users.Commands.DeleteUser;
using UserMessages;

namespace Surveys.Infrastructure.Kafka.UsersConsumer;

public class KafkaUserConsumerRouter
{
    private readonly ISender _sender;
    private readonly ILogger<KafkaUserConsumerRouter> _logger;
    
    public KafkaUserConsumerRouter(ISender sender, ILogger<KafkaUserConsumerRouter> logger)
    {
        _sender = sender;
        _logger = logger;
    }

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
            case ("UserCreated"):
                await CreateUserAsync(message);
                
                break;
            
            case ("UserDeleted"):
                await DeleteUserAsync(message);
                
                break;
        }
    }

    private async Task CreateUserAsync(Message<Null, string> message)
    {
        var createUserMessage = JsonConvert
            .DeserializeObject<CreateUserMessage>(message.Value)!;

        await _sender.Send(new CreateUserCommand(createUserMessage));
        
        _logger.LogInformation($"User with ID {createUserMessage.Id} created.");
    }

    private async Task DeleteUserAsync(Message<Null, string> message)
    {
        var userId = new Guid(message.Value);

        try
        {
            await _sender.Send(new DeleteUserCommand(userId));

            _logger.LogInformation($"User with ID {userId.ToString()} deleted.");
        }
        catch (UserNotFound)
        {
            _logger.LogError($"User with ID {userId} was not found.");
        }
    }
}