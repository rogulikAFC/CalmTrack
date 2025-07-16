using MediatR;
using Surveys.Application.Features.DTOs.Surveys;
using Surveys.Application.Features.Exceptions;
using Surveys.Application.UnitOfWork;

namespace Surveys.Application.Features.FormInstances.Commands.AddFormInstance;

public class AddFormInstanceCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddFormInstanceCommand, FormInstanceDto>
{
    public async Task<FormInstanceDto> Handle(AddFormInstanceCommand request, CancellationToken cancellationToken)
    {
        var doesUserExist = await unitOfWork.UserRepository
            .DoesUserExist(request.FormInstanceForCreateDto.UserId);

        if (!doesUserExist)
        {
            throw new UserNotFound(
                request.FormInstanceForCreateDto.UserId);
        }
        
        var formInstance = request.FormInstanceForCreateDto
            .MapToFormInstance();

        await unitOfWork.FormInstanceRepository
            .AddFormInstanceAsync(formInstance);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var createdFormInstance = await unitOfWork.FormInstanceRepository
            .GetFormInstanceByIdAsync(formInstance.Id);

        if (createdFormInstance == null)
        {
            throw new FormInstanceNotFound(formInstance.Id);
        }

        return FormInstanceDto.MapFromFormInstance(createdFormInstance);
    }
}