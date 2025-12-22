using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace InnoclinicAutho.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : BaseEntity
{
    protected readonly IApiDbContext _dbContext;
    private bool disposed = false;

    public BaseRepository(IApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        var entries = _dbContext.ChangeTracker.Entries<T>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            entry.Entity.UpdateDateTime = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
            _dbContext.Dispose();

        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
