using Surveys.Domain.Survey;

namespace Surveys.Domain.User
{
    public class User
    {
        public Guid Id { get; set; }

        public List<FormInstance> InstancesOfCompletedSurvays { get; set; } = [];
    }
}
