using UserMessages;

namespace Mail.Application.MailRendering;

public interface IMailRenderer
{
    Task<string> RenderUserCreatedMailAsync(
        string templateFileName, CreateUserMessage createUserMessage);

    Task<string> RenderUserDeletedMailAsync(string templateFileName, Guid userId);
}