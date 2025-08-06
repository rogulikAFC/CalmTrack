using Mail.Application.UnitOfWork;
using Mail.Application.UnitOfWork.Repositories;
using Mail.Infrastructure.Persistence.UnitOfWork.Repositories;

namespace Mail.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork(MailDbContext context)
    : IUnitOfWork
{
    public IUserRepository Users { get; }
        = new UserRepository(context);
    
    public ITemplateRepository Templates { get; }
        = new TemplateRepository(context);
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}