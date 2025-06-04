namespace Surveys.Application.Features.Exceptions
{
    public class SurveyNotFound(Guid SurveyId)
        : Exception($"Survey with ID {SurveyId} not found")
    { }
}
