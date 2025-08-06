using Mail.Domain.User;

namespace Mail.Application.UnitOfWork.Repositories;

public interface IUserRepository
{
    void AddUser(User user);
    
    void RemoveUser(User user);
    
    Task<User?> GetUserByIdAsync(Guid id);
}