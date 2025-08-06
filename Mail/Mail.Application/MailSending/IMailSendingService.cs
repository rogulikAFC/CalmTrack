using UserMessages;

namespace Mail.Application.MailSending;

public interface IMailSendingService
{
    /**
     * Sends mail about user creating.
     * 
     * <param name="createUserMessage">Create user message from the Kafka.</param>
     */
    public Task SendUserCreatedMailAsync(CreateUserMessage createUserMessage);

    /**
     * Sends mail about user removing.
     *
     * <param name="userId">ID of user (the Kafka message value)</param>
     */
    public Task SendUserDeletedMailAsync(Guid userId);
}