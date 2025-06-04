using MediatR;
using Surveys.Application.Features.Exceptions;
using Surveys.Application.UnitOfWork;

namespace Surveys.Application.Features.Surveys.Commands.RemoveSurvey
{
    public class RemoveSurveyCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<RemoveSurveyCommand>
    {
        public async Task Handle(
            RemoveSurveyCommand request, CancellationToken cancellationToken)
        {
            var survey = await unitOfWork.SurveyRepository
                .GetSurveyByIdAsync(request.SurveyId);

            if (survey == null)
            {
                throw new SurveyNotFound(request.SurveyId);
            }

            unitOfWork.SurveyRepository.RemoveSurvey(survey);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
