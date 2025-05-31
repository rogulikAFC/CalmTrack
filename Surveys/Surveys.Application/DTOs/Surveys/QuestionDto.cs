using Surveys.Domain.Survey;

namespace Surveys.Application.DTOs.Surveys
{
    public class QuestionDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; } = null!;

        public List<AnswerDto> Answers { get; set; }
            = new List<AnswerDto>();

        public static QuestionDto MapFromQuestion(
            Question question)
        {
            return new QuestionDto
            {
                Id = question.Id,
                Text = question.QuestionText,
                Answers = question.Answers
                    .Select(AnswerDto.MapFromAnswer)
                    .ToList(),
            };
        }
    }
}
