namespace InnoclinicAutho.Domain.Entities;

using InnoclinicAutho.Domain.Common;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public UserRoles Role { get; set; } = UserRoles.Patient;
}
