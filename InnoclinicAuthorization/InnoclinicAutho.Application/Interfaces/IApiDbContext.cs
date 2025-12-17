namespace InnoclinicAutho.Application.Interfaces;

using InnoclinicAutho.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

public interface IApiDbContext
{
    public DbSet<User> Users { get; }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

