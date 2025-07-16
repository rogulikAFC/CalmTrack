using Surveys.Domain.Survey;

namespace Surveys.Application.Features.DTOs.Surveys
{
    public class FormInstanceForCreateDto
    {
        public Guid UserId { get; set; }

        public DateOnly DateOnly { get; set; }

        public List<Guid> AnswerIds { get; set; } = [];

        public FormInstance MapToFormInstance()
        {
            var formInstance = new FormInstance
            {
                UserId = UserId,
                DateOnly = DateOnly
            };

            formInstance.UserAnswers = (IEnumerable<UserAnswer>)AnswerIds.Select(answerId =>
                new UserAnswer
                {
                    AnswerId = answerId,
                    UserId = UserId,
                    FormInstanceId = formInstance.Id
                });

            return formInstance;
        }
    }
}
