namespace InnoclinicAutho.Application.Interfaces.Repositories;

using InnoclinicAutho.Domain.Entities;

public interface IUserRepository : IBaseRepository<User>
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    void Insert(User user);
    void Update(User user);
    Task Delete(Guid id);
}
