using Surveys.Application.UnitOfWork;
using Surveys.Application.UnitOfWork.Repositories;
using Surveys.Infrastructure.Persistence.UnitOfWork.Repositories;

namespace Surveys.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SurveysDbContext _context;

        public UnitOfWork(SurveysDbContext context)
        {
            _context = context;
        }

        public ISurveyRepository SurveyRepository 
            => new SurveyRepository(_context);

        public IUserRepository UserRepository
            => new UserRepository(_context);

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
