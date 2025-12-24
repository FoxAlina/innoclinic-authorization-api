using InnoclinicAutho.Domain.Common;

namespace InnoclinicAutho.Application.Interfaces.Repositories;

public interface IBaseRepository<T> where T : IBaseEntity
{
    Task SaveAsync(CancellationToken cancellationToken = default);
}
