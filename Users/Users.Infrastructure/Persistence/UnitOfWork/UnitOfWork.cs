using Application.UnitOfWork;
using Application.UnitOfWork.Repositories;
using Infrastructure.Persistence.UnitOfWork.Repositories;

namespace Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CalmTrackDbContext _context;

        public UnitOfWork(CalmTrackDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => new UserRepository(_context);

        public IRoleRepository RoleRepository => new RoleRepository(_context);

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
