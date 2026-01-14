using InnoclinicAutho.Domain.Entities;

namespace InnoclinicAutho.Application.Interfaces.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<IEnumerable<Role>> GetAllAsync();
    Task<Role?> GetByIdAsync(Guid id);
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    void Insert(Role role);
    void Update(Role role);
    Task DeleteAsync(Guid id);
}
