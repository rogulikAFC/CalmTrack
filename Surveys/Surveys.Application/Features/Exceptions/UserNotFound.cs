namespace Surveys.Application.Features.Exceptions;

public class UserNotFound(Guid id) : Exception($"User with id ${id.ToString()} not found")
{ }