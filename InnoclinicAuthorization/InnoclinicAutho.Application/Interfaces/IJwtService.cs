namespace InnoclinicAutho.Application.Interfaces;

using InnoclinicAutho.Domain.Common;

public interface IJwtService
{
	public string GenerateToken(Guid userId, string email, IList<string> userRoles);
}

