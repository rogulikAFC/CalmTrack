using Surveys.Domain.Survey;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Application.DTOs.Surveys
{
    public class QuestionForCreateDto
    {
        [Required]
        public string Text { get; set; } = null!;

        [Required]
        public List<AnswerForCreateDto> Answers { get; set; }
            = new List<AnswerForCreateDto>();

        [Required]
        public int SerialNumber { get; set; }

        public Question MapToQuestion()
        {
            return new Question
            {
                SerialNumber = SerialNumber,
                QuestionText = Text,
                Answers = Answers
                    .Select(answer => answer.MapToAnswer())
                    .ToList(),
            };
        }
    }
}
