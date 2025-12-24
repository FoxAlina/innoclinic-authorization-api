namespace InnoclinicAutho.Persistence.Context;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Domain.Common;
using InnoclinicAutho.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApiDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IApiDbContext
{
	public ApiDbContext() : base() { }
	public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
	
	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		options.UseNpgsql();
	}

	public DbSet<User> Users { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(e => e.Id);

			entity.Property(e => e.Email)
				.IsRequired()
				.HasMaxLength(100);

			entity.Property(e => e.FirstName)
				.IsRequired()
				.HasMaxLength(50);

			entity.Property(e => e.LastName)
				.IsRequired()
				.HasMaxLength(50);

			entity.Property(e => e.PasswordHash)
				.IsRequired();

			entity.Property(e => e.IsDeleted)
				.HasDefaultValue(false);

			entity.Property(e => e.CreateDateTime)
				.HasDefaultValueSql("CURRENT_TIMESTAMP");

			entity.HasIndex(e => e.Email)
				.IsUnique()
				.HasDatabaseName("IX_Users_Email");
		});

		base.OnModelCreating(modelBuilder);
	}
}
