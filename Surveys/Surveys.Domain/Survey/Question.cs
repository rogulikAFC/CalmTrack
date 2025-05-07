namespace Surveys.Domain.Survey
{
    public class Question
    {
        // Survey and serial number must be unique
        public Guid Id { get; set; }

        public Guid SurveyId { get; set; }

        public Survey Survey { get; set; } = null!;
        
        public int SerialNumber { get; set; }

        public string QuestionText { get; set; } = null!;

        public List<Answer> Answers { get; set; } = [];
    }
}
