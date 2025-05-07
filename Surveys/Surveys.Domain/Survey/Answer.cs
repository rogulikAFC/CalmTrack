namespace Surveys.Domain.Survey
{
    public class Answer
    {
        // Answer text and question combination must be unique
        public Guid AnswerId { get; set; }

        public Guid QuestionId { get; set; }

        public Question Question { get; set; } = null!;

        public string AnswerText { get; set; } = null!;

        public int Value { get; set; }
    }
}
