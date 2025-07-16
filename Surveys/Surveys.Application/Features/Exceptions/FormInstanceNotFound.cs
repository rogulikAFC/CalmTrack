namespace Surveys.Application.Features.Exceptions;

public class FormInstanceNotFound(Guid Id) 
    : Exception($"The form instance with id {Id.ToString()} not found");