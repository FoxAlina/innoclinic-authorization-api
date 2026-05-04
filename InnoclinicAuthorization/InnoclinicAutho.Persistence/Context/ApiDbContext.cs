namespace InnoclinicAutho.Persistence.Context;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Domain.Common;
using InnoclinicAutho.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ApiDbContext : DbContext, IApiDbContext
{
    public ApiDbContext() : base() { }
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

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

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false);

            entity.Property(e => e.CreateDateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasIndex(e => e.RoleName)
                .IsUnique()
                .HasDatabaseName("IX_Roles_Name");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(t => t.User).WithMany(e => e.UserRoles).HasForeignKey(k => k.UserId);
            entity.HasOne(t => t.Role).WithMany(e => e.UserRoles).HasForeignKey(k => k.RoleId);

            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false);

            entity.Property(e => e.CreateDateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasIndex(e => e.Id)
                .IsUnique()
                .HasDatabaseName("IX_UserRoles_Id");
        });

        base.OnModelCreating(modelBuilder);
    }

}
