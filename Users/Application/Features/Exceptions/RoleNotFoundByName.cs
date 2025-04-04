namespace Application.Features.Exceptions
{
    public class RoleNotFoundByName(string roleName)
        : Exception($"Role with name \"{roleName}\" was not found.")
    { }
}
