using Surveys.Domain.Survey;

namespace Surveys.Application.UnitOfWork.Repositories
{
    public interface ISurveyRepository
    {
        Task<Survey?> GetSurveyByIdAsync(Guid id);

        void AddSurvey(Survey survey);

        Task<FormInstance?> GetFormInstanceByIdAsync(Guid id);

        Task<List<FormInstance>> ListFormInstancesOfUser(Guid userId);

        // Score must be counted and result must be defined in this method
        Task AddFormInstance(FormInstance formInstance);

        void RemoveSurvey(Survey survey);

        Task<List<Survey>> ListSurveys(
            int pageSize, int pageNum, string? query, bool isArhieved);
    }
}
