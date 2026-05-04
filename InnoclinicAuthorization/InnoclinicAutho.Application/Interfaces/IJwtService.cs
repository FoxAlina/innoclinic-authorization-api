using System.Security.Claims;

namespace InnoclinicAutho.Application.Interfaces;

public interface IJwtService
{
    public string GenerateToken(Guid userId, string email, List<string> userRoles);
    public DateTime ReadTokenExpiryTime(string token);
    public Guid ReadTokenUserId(string token);
    public IEnumerable<Claim> ReadTokenClaims(string token);
}

