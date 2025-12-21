namespace InnoclinicAutho.Application.Interfaces;

using InnoclinicAutho.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public interface IApiDbContext: IDisposable
{
    public DbSet<User> Users { get; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

