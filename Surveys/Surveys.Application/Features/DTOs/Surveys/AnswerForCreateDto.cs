using Surveys.Domain.Survey;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Application.Features.DTOs.Surveys
{
    public class AnswerForCreateDto
    {
        [Required]
        [MaxLength(32)]
        public string Text { get; set; } = null!;

        [Required]
        public int Value { get; set; }

        public Answer MapToAnswer()
        {
            return new Answer
            {
                AnswerText = Text,
                Value = Value
            };
        }
    }
}
