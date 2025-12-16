
using InnoclinicAutho.Domain.Common;

namespace InnoclinicAutho.Application.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(Guid userId, string email, IList<string> userRoles);
    }
}
