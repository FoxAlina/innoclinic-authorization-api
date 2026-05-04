namespace InnoclinicAutho.Application.Interfaces;

using InnoclinicAutho.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public interface IApiDbContext: IDisposable
{
    public DbSet<User> Users { get; }
    public DbSet<Role> Roles { get; }
    public DbSet<UserRole> UserRoles { get; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public ChangeTracker ChangeTracker { get; }
    
}

