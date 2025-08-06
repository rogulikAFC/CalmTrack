using Mail.Application.MailSending;
using Microsoft.Extensions.DependencyInjection;

namespace Mail.Infrastructure.MailSending;

public static class AddMailSendingServiceExtension 
{
    public static void AddMailSendingService(
        this IServiceCollection services)
    {
        services.AddSingleton<IMailSendingService, MailSendingService>();
    }
}