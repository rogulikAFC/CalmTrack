using Mail.Application.MailRendering;
using Scriban;
using UserMessages;

namespace Mail.Infrastructure.MailRendering;

public class MailRenderer(string templatesPath) : IMailRenderer
{
    public async Task<string> RenderUserCreatedMailAsync(
        string templateFileName, CreateUserMessage createUserMessage)
    {
        var reader = new StreamReader(
            Path.Combine(templatesPath, templateFileName));
        
        var mailTemplateText = await reader.ReadToEndAsync();

        var template = Template.Parse(mailTemplateText);
        
        return await template.RenderAsync(new
        {
            User = createUserMessage
        });
    }

    public async Task<string> RenderUserDeletedMailAsync(string templateFileName, Guid userId)
    {
        var reader = new StreamReader(
            Path.Combine(templatesPath, templateFileName));
        
        var mailTemplateText = await reader.ReadToEndAsync();

        var template = Template.Parse(mailTemplateText);
        
        return await template.RenderAsync(new
        {
            UserId = userId
        });
    }
}