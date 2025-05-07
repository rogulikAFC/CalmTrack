namespace Surveys.Domain.Survey
{
    public class Survey
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public List<Question> Questions { get; set; } = [];

        public List<Scale> Scales { get; set; } = [];
    }
}
