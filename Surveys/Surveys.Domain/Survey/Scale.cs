namespace Surveys.Domain.Survey
{
    public class Scale
    {
        public Guid Id { get; set; }

        // Value and survey id must be unique
        public Guid SurveyId { get; set; }

        public Survey Survey { get; set; } = null!;

        public int From { get; set; }

        public int To { get; set; }

        public string Value { get; set; } = null!;

        public List<FormInstance> FormInstances { get; set; } 
            = new List<FormInstance>();
    }
}
