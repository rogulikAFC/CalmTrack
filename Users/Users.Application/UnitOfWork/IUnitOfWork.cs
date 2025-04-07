using Application.UnitOfWork.Repositories;

namespace Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IRoleRepository RoleRepository { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken); 
    }
}
