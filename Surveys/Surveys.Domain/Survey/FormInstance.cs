namespace Surveys.Domain.Survey
{
    public class FormInstance
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User.User User { get; set; } = null!;

        public DateOnly DateOnly { get; set; }

        public List<UserAnswer> UserAnswers { get; set; } = [];

        public int Points => 
            UserAnswers.Select(a => a.Answer.Value).Count();
    }
}
