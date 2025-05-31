using Surveys.Domain.Survey;

namespace Surveys.Application.DTOs.Surveys
{
    public class FormInstanceDto
    {
        public Guid Id { get; set; }

        public DateOnly Date { get; set; }

        public int Points { get; set; }

        public string? Result { get; set; }

        public static FormInstanceDto MapFromFormInstance(
            FormInstance formInstance)
        {
            return new FormInstanceDto
            {
                Id = formInstance.Id,
                Date = formInstance.DateOnly,
                Points = formInstance.Points,
                Result = formInstance.Result?.Value,
            };
        }
    }
}
