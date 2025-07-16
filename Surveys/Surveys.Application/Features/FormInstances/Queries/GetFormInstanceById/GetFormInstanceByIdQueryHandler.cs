using MediatR;
using Surveys.Application.Features.DTOs.Surveys;
using Surveys.Application.Features.Exceptions;
using Surveys.Application.UnitOfWork;
using Surveys.Domain.Survey;

namespace Surveys.Application.Features.FormInstances.Queries.GetFormInstanceById;

public class GetFormInstanceByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetFormInstanceByIdQuery, FormInstanceDto>
{
    public async Task<FormInstanceDto> Handle(
        GetFormInstanceByIdQuery request, CancellationToken cancellationToken)
    {
        var formInstance = await unitOfWork.FormInstanceRepository
            .GetFormInstanceByIdAsync(request.Id);

        if (formInstance == null)
        {
            throw new FormInstanceNotFound(request.Id);
        }

        var formInstanceDto = FormInstanceDto
            .MapFromFormInstance(formInstance);

        return formInstanceDto;
    }
}