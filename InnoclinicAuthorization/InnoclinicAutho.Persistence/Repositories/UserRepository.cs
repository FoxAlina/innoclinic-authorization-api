namespace InnoclinicAutho.Persistence.Repositories;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Domain.Entities;
using InnoclinicAutho.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class UserRepository : IUserRepository, IDisposable
{
    private readonly IApiDbContext _dbContext;
    private bool disposed = false;

    public UserRepository()
    {
        _dbContext = new ApiDbContext();
    }

    public UserRepository(IApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Delete(Guid userId)
    {
        User? user= await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        
        if (user != null)
            _dbContext.Users.Remove(user);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public void Insert(User user)
    {
        _dbContext.Users.Add(user);
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Update(User user)
    {
        _dbContext.Users.Entry(user).State = EntityState.Modified;
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
