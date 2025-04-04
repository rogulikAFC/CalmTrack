using Application.Features.Exceptions;
using Application.UnitOfWork.Repositories;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.UnitOfWork.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly CalmTrackDbContext _context;

        public RoleRepository(CalmTrackDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetRoleByIdAsync(Guid roleId)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(role => role.Id == roleId);
        }

        public async Task<Role?> GetRoleByNameAsync(string name)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(role => role.Name == name);
        }
    }
}
