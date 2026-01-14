namespace InnoclinicAutho.Persistence.Repositories;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IApiDbContext dbContext) : base(dbContext) { }

    public async Task DeleteAsync(Guid id)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user != null)
            _dbContext.Users.Remove(user);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public void Insert(User user)
    {
        _dbContext.Users.Add(user);
    }

    public void Update(User user)
    {
        _dbContext.Users.Entry(user).State = EntityState.Modified;
    }

}
