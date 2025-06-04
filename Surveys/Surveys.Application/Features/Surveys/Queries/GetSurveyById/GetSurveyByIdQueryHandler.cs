using MediatR;
using Surveys.Application.Features.DTOs.Surveys;
using Surveys.Application.Features.Exceptions;
using Surveys.Application.UnitOfWork;

namespace Surveys.Application.Features.Surveys.Queries.GetSurveyById
{
    public class GetSurveyByIdQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetSurveyByIdQuery, SurveyDto>
    {
        public async Task<SurveyDto> Handle(
            GetSurveyByIdQuery request, CancellationToken cancellationToken)
        {
            var survey = await unitOfWork.SurveyRepository
                .GetSurveyByIdAsync(request.SurveyId);

            if (survey == null)
            {
                throw new SurveyNotFound(request.SurveyId);
            }

            return SurveyDto.MapFromSurvey(survey);
        }
    }
}
