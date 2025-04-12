namespace Application.Features.Exceptions
{
    public class RoleNotFoundById(Guid roleId) 
        : Exception($"Role with id {roleId} was not found.")
    { }
}
