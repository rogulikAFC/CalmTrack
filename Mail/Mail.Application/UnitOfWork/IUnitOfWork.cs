using Mail.Application.UnitOfWork.Repositories;

namespace Mail.Application.UnitOfWork;

public interface IUnitOfWork
{
   IUserRepository Users { get; }
   
   ITemplateRepository Templates { get; }

   Task SaveChangesAsync();
}