namespace Application.Features.Exceptions
{
    public class UserHasNotPermission(Guid userId)
        : Exception($"User with ID {userId} has not permission to access this endpoint.")
    { }
}
