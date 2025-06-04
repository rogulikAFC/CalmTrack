using MediatR;
using Surveys.Application.UnitOfWork;

namespace Surveys.Application.Features.Surveys.Commands.CreateSurvey
{
    public class CreateSurveyCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<CreateSurveyCommand, Guid>
    {
        public async Task<Guid> Handle(
            CreateSurveyCommand request, CancellationToken cancellationToken)
        {
            var survey = request.SurveyForCreateDto
                .MapToSurvey();

            unitOfWork.SurveyRepository.AddSurvey(survey);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return survey.Id;
        }
    }
}
