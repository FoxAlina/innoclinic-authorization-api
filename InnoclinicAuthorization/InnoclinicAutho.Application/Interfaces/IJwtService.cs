namespace InnoclinicAutho.Application.Interfaces;

public interface IJwtService
{
    public string GenerateToken(Guid userId, string email, List<string> userRoles);
}

