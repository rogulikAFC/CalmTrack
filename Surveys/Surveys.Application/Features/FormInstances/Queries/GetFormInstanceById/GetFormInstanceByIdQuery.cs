using MediatR;
using Surveys.Application.Features.DTOs.Surveys;
using Surveys.Domain.Survey;

namespace Surveys.Application.Features.FormInstances.Queries.GetFormInstanceById;

public record GetFormInstanceByIdQuery(Guid Id)
    : IRequest<FormInstanceDto>;