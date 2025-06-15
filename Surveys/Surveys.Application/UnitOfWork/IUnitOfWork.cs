using Surveys.Application.UnitOfWork.Repositories;

namespace Surveys.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        ISurveyRepository SurveyRepository { get; }

        IFormInstanceRepository FormInstanceRepository { get; }
        
        IUserRepository UserRepository { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
