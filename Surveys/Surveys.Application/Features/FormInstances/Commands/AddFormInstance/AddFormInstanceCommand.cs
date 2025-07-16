using MediatR;
using Surveys.Application.Features.DTOs.Surveys;

namespace Surveys.Application.Features.FormInstances.Commands.AddFormInstance;

public record AddFormInstanceCommand(FormInstanceForCreateDto FormInstanceForCreateDto)
    : IRequest<FormInstanceDto>;