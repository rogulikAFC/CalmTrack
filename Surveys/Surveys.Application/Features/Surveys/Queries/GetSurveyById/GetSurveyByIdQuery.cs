using MediatR;
using Surveys.Application.Features.DTOs.Surveys;

namespace Surveys.Application.Features.Surveys.Queries.GetSurveyById
{
    public record GetSurveyByIdQuery(Guid SurveyId)
        : IRequest<SurveyDto>;
}
