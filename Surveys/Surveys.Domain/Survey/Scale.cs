namespace Surveys.Domain.Survey
{
    public class Scale
    {
        // Value and survay id must be unique
        public Guid SurveyId { get; set; }

        public Survey Survey { get; set; } = null!;

        public int From { get; set; }

        public int To { get; set; }

        public string Value { get; set; } = null!;
    }
}
