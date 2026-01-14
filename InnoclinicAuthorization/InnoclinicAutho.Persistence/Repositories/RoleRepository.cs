using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoclinicAutho.Persistence.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(IApiDbContext dbContext) : base(dbContext) { }

    public async Task DeleteAsync(Guid id)
    {
        Role? role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);

        if (role != null)
            _dbContext.Roles.Remove(role);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _dbContext.Roles.ToListAsync();
    }

    public async Task<Role?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleName == name);
    }

    public void Insert(Role role)
    {
        _dbContext.Roles.Add(role);
    }

    public void Update(Role role)
    {
        _dbContext.Roles.Entry(role).State = EntityState.Modified;
    }
}
