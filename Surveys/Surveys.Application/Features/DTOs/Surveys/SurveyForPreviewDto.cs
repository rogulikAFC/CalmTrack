using Surveys.Domain.Survey;

namespace Surveys.Application.Features.DTOs.Surveys
{
    public class SurveyForPreviewDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsArhieved { get; set; }

        public static SurveyForPreviewDto MapFromSurvey(Survey survey)
        {
            return new SurveyForPreviewDto
            {
                Id = survey.Id,
                Name = survey.Name,
                IsArhieved = survey.IsArchived
            };
        }
    }
}
