using MediatR;
using Surveys.Application.Features.DTOs.Surveys;
using Surveys.Application.UnitOfWork;

namespace Surveys.Application.Features.Surveys.Queries.ListSurveys
{
    public class ListSurveysQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<ListSurveysQuery, List<SurveyForPreviewDto>>
    {
        public async Task<List<SurveyForPreviewDto>> Handle(
            ListSurveysQuery request, CancellationToken cancellationToken)
        {
            var surveys = await unitOfWork.SurveyRepository
                .ListSurveys(
                    request.PageSize, request.PageNum,
                    request.Query, request.IsArhieved);

            return surveys
                .Select(SurveyForPreviewDto.MapFromSurvey)
                .ToList();
        }
    }
}
