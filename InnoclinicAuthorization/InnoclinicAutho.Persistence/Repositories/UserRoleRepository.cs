using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoclinicAutho.Persistence.Repositories;

public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(IApiDbContext dbContext) : base(dbContext) { }

    public async Task DeleteAsync(Guid id)
    {
        UserRole? userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(u => u.Id == id);

        if (userRole != null)
            _dbContext.UserRoles.Remove(userRole);
    }

    public async Task<IEnumerable<UserRole>> GetAllByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserRoles.Where(t => t.RoleId == roleId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserRole>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserRoles.Where(t => t.UserId == userId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>> GetAllRoleNamesByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserRoles.Join(
            _dbContext.Roles,
            ur => ur.RoleId,
            r => r.Id,
            (ur, r) => new { ur, r }).
            Where(joined => joined.ur.UserId == userId)
            .Select(joined => joined.r.RoleName)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserRole?> GetByUserIdRoleNameAsync(Guid userId, string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserRoles.Join(
            _dbContext.Roles,
            ur => ur.RoleId,
            r => r.Id,
            (ur, r) => new { ur, r})
            .Where(joined => joined.r.RoleName == name)
            .Select(joined => joined.ur)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public void Insert(UserRole userRole)
    {
        _dbContext.UserRoles.Add(userRole);
    }

    public void Update(UserRole userRole)
    {
        _dbContext.UserRoles.Entry(userRole).State = EntityState.Modified;
    }
}
