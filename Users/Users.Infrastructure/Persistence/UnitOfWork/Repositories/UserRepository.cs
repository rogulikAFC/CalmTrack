using Application.UnitOfWork.Repositories;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.UnitOfWork.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CalmTrackDbContext _context;

        public UserRepository(CalmTrackDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Add(user);
        }

        public void DeleteUser(User user)
        {
            _context.Remove(user);
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(user => user.Role)
                .FirstOrDefaultAsync(user => user.Id == id);
        }
    }
}
