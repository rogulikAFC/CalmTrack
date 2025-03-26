using Domain.User;

namespace Application.UnitOfWork.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid id);

        void AddUser(User user);

        void DeleteUser(User user);
    }
}
