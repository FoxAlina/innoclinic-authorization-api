namespace InnoclinicAutho.Application.Interfaces.Repositories;

using InnoclinicAutho.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid userId);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    void Insert(User user);
    void Update(User user);
    Task Delete(Guid userId);
    Task SaveAsync(CancellationToken cancellationToken = default);
}
