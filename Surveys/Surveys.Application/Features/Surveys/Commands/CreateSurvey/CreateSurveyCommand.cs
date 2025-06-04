using MediatR;
using Surveys.Application.Features.DTOs.Surveys;

namespace Surveys.Application.Features.Surveys.Commands.CreateSurvey
{
    public record CreateSurveyCommand(SurveyForCreateDto SurveyForCreateDto)
        : IRequest<Guid>;
}
