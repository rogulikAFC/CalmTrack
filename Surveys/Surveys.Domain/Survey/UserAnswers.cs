namespace Surveys.Domain.Survey
{
    public class UserAnswer
    {
        public Guid AnswerId { get; set; }

        public Answer Answer { get; set; } = null!;

        public Guid UserId { get; set; }

        public User.User User { get; set; } = null!;

        public Guid FormInstanceId { get; set; }

        public FormInstance FormInstance { get; set; } = null!;
    }
}
