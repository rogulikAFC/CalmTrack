using Surveys.Domain.User;

namespace Surveys.Application.UnitOfWork.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(Guid id);

        void RemoveUser(User user);

        void AddUser(User user);
    }
}
