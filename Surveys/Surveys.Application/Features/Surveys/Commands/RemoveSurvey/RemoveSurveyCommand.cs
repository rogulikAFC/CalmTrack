using MediatR;

namespace Surveys.Application.Features.Surveys.Commands.RemoveSurvey
{
    public record RemoveSurveyCommand(Guid SurveyId)
        : IRequest;
}
