using Surveys.Domain.Survey;

namespace Surveys.Application.UnitOfWork.Repositories
{
    public interface ISurveyRepository
    {
        Task<Survey?> GetSurveyByIdAsync(Guid id);

        void AddSurvey(Survey survey);

        void RemoveSurvey(Survey survey);

        Task<List<Survey>> ListSurveys(
            int pageSize, int pageNum, string? query, bool isArhieved);
    }
}
