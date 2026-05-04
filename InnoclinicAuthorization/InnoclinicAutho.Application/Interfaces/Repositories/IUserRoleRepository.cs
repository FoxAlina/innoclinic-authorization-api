using InnoclinicAutho.Domain.Entities;

namespace InnoclinicAutho.Application.Interfaces.Repositories;

public interface IUserRoleRepository : IBaseRepository<UserRole>
{
    Task<IEnumerable<UserRole>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> GetAllRoleNamesByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<UserRole>> GetAllByRoleIdAsync(Guid roleId, CancellationToken cancellationToken = default);
    Task<UserRole?> GetByUserIdRoleNameAsync(Guid userId, string name, CancellationToken cancellationToken = default);
    void Insert(UserRole userRole);
    void Update(UserRole userRole);
    Task DeleteAsync(Guid id);
}
