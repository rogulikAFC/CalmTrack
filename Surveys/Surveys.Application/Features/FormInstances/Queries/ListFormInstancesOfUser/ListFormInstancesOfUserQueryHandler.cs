using MediatR;
using Surveys.Application.Features.DTOs.Surveys;
using Surveys.Application.Features.Exceptions;
using Surveys.Application.UnitOfWork;

namespace Surveys.Application.Features.FormInstances.Queries.ListFormInstancesOfUser;

public class ListFormInstancesOfUserQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ListFormInstancesOfUserQuery, IEnumerable<FormInstanceDto>>
{
    public async Task<IEnumerable<FormInstanceDto>> Handle(
        ListFormInstancesOfUserQuery request, CancellationToken cancellationToken)
    {
        var doesUserExist = await unitOfWork.UserRepository
            .DoesUserExist(request.UserId);

        if (!doesUserExist)
        {
            throw new UserNotFound(request.UserId);
        }

        var formInstances = await unitOfWork.FormInstanceRepository
            .ListFormInstancesOfUser(request.UserId);

        return formInstances.Select(FormInstanceDto.MapFromFormInstance);
    }
}