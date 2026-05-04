using InnoclinicAutho.Domain.Common;

namespace InnoclinicAutho.Domain.Entities;

public class Role : BaseEntity
{
    public string RoleName { get; set; }

    public List<UserRole> UserRoles { get; set; }
}
