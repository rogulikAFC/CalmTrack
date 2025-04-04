using Domain.User;

namespace Application.UnitOfWork.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleByIdAsync(Guid roleId);

        Task<Role?> GetRoleByNameAsync(string name);
    }
}
