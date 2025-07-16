using Surveys.Domain.Survey;

namespace Surveys.Application.UnitOfWork.Repositories
{
    public interface IFormInstanceRepository
    {
        Task<FormInstance?> GetFormInstanceByIdAsync(Guid id);

        Task<List<FormInstance>> ListFormInstancesOfUser(Guid userId);

        // Score must be counted and result must be defined in this method
        Task AddFormInstanceAsync(FormInstance formInstance);
    }
}
