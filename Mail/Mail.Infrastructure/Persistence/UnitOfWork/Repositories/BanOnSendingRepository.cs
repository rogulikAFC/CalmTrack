using Mail.Application.UnitOfWork.Repositories;
using Mail.Domain.Template;
using Mail.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Mail.Infrastructure.Persistence.UnitOfWork.Repositories;

public class BanOnSendingRepository(MailDbContext context) 
    : IBanOnSendingRepository
{
    public void BanSendingToUser(User user, Template template)
    {
        var banOnSending = new BanOnSending
        {
            UserId = user.Id,
            TemplateName = template.Name,
        };
        
        context.BansOnSending.Add(banOnSending);
    }

    public async Task AllowSendingToUser(User user, Template template)
    {
        var banOnSending = await context.BansOnSending
            .FirstOrDefaultAsync(bos =>
                bos.UserId == user.Id
                && bos.TemplateName == template.Name);

        if (banOnSending != null)
        {
            context.BansOnSending.Remove(banOnSending);
        }
    }

    public async Task<bool> IsSendingToUserAllowed(User user, string templateName)
    {
        return !await context.BansOnSending.AnyAsync(bos =>
            bos.UserId == user.Id
            && bos.TemplateName == templateName);
    }
}