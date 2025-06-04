using Surveys.Domain.Survey;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Application.Features.DTOs.Surveys
{
    public class SurveyForCreateDto
    {
        [Required]
        [MaxLength(32)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(512)]
        public string Description { get; set; } = null!;

        [Required]
        public List<QuestionForCreateDto> Questions { get; set; }
            = new List<QuestionForCreateDto>();

        [Required]
        public List<ScaleForCreateDto> Scales { get; set; }
            = new List<ScaleForCreateDto>();

        public Survey MapToSurvey()
        {
            return new Survey
            {
                Name = Name,
                Description = Description,
                Questions = Questions
                    .Select(question => question.MapToQuestion())
                    .ToList(),
                Scales = Scales
                    .Select(scale => scale.MapToScale())
                    .ToList()
            };
        }
    }
}
