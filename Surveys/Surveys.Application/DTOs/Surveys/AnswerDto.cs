using Surveys.Domain.Survey;

namespace Surveys.Application.DTOs.Surveys
{
    public class AnswerDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; } = null!;

        public static AnswerDto MapFromAnswer(Answer answer)
        {
            return new AnswerDto
            {
                Id = answer.Id,
                Text = answer.AnswerText,
            };
        }
    }
}
