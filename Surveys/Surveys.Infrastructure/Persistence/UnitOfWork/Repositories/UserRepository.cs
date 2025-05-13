using Microsoft.EntityFrameworkCore;
using Surveys.Application.UnitOfWork.Repositories;
using Surveys.Domain.User;

namespace Surveys.Infrastructure.Persistence.UnitOfWork.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SurveysDbContext _context;

        public UserRepository(SurveysDbContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Add(user);
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _context.Users

                .Include(user => user.InstancesOfCompletedSurveys)

                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public void RemoveUser(User user)
        {
            _context.Remove(user);
        }
    }
}
