using Surveys.Domain.Survey;
using System.ComponentModel.DataAnnotations;

namespace Surveys.Application.Features.DTOs.Surveys
{
    public class ScaleForCreateDto
    {
        [Required]
        public int From { get; set; }

        [Required]
        public int To { get; set; }

        [Required]
        public string Value { get; set; } = null!;

        public Scale MapToScale()
        {
            return new Scale
            {
                From = From,
                To = To,
                Value = Value
            };
        }
    }
}
