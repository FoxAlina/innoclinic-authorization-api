namespace InnoclinicAutho.Application.Interfaces;

using InnoclinicAutho.Domain.Common;

public interface IJwtService
{
    public string GenerateToken(Guid userId, string email, UserRoles _userRole);
}

