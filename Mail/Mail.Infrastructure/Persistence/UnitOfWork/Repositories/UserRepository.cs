using Mail.Application.UnitOfWork.Repositories;
using Mail.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Mail.Infrastructure.Persistence.UnitOfWork.Repositories;

public class UserRepository(MailDbContext context) 
    : IUserRepository
{
    public void AddUser(User user)
    {
        context.Users.Add(user);
    }

    public void RemoveUser(User user)
    {
        context.Users.Remove(user);
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}