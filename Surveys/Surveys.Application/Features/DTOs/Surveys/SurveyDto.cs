using Surveys.Domain.Survey;

namespace Surveys.Application.Features.DTOs.Surveys
{
    public class SurveyDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsArchived { get; set; }

        public List<QuestionDto> Questions { get; set; } = null!;

        public static SurveyDto MapFromSurvey(Survey survey)
        {
            return new SurveyDto
            {
                Id = survey.Id,
                Name = survey.Name,
                Description = survey.Description,
                IsArchived = survey.IsArchived,
                Questions = survey.Questions
                    .Select(QuestionDto.MapFromQuestion)
                    .ToList()
            };
        }
    }
}
