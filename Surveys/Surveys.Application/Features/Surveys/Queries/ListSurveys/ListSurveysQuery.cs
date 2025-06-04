using MediatR;
using Surveys.Application.Features.DTOs.Surveys;

namespace Surveys.Application.Features.Surveys.Queries.ListSurveys
{
    public record ListSurveysQuery(
        string? Query, bool IsArhieved, int PageNum, int PageSize)
        : IRequest<List<SurveyForPreviewDto>>;
}
