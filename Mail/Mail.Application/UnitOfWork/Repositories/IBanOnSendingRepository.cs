using Mail.Domain.Template;
using Mail.Domain.User;

namespace Mail.Application.UnitOfWork.Repositories;

public interface IBanOnSendingRepository
{
    void BanSendingToUser(User user, Template template);

    Task AllowSendingToUser(User user, Template template);

    Task<bool> IsSendingToUserAllowed(User user, string templateName);
}