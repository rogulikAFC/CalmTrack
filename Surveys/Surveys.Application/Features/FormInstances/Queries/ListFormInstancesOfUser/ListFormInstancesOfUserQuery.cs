using MediatR;
using Surveys.Application.Features.DTOs.Surveys;

namespace Surveys.Application.Features.FormInstances.Queries.ListFormInstancesOfUser;

public record ListFormInstancesOfUserQuery(Guid UserId)
    : IRequest<IEnumerable<FormInstanceDto>>;