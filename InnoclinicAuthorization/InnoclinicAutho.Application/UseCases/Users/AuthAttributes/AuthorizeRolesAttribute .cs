using InnoclinicAutho.Domain.Common;

namespace InnoclinicAutho.Application.UseCases.Users.AuthAttributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class AuthorizeRolesAttribute : Attribute
{
    public UserRoles[] Roles { get; }

    public AuthorizeRolesAttribute(params UserRoles[] roles)
    {
        Roles = roles;
    }
}
