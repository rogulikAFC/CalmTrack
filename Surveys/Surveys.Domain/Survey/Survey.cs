namespace Surveys.Domain.Survey
{
    public class Survey
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool IsArchived { get; set; } = false;

        public List<Question> Questions { get; set; } = [];

        public List<Scale> Scales { get; set; } = [];
    }
}
