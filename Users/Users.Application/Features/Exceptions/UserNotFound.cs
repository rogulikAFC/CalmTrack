namespace Application.Features.Exceptions
{
    public class UserNotFound(Guid userId) 
        : Exception($"User with id {userId} was not found.")
    { }
}
