using InnoclinicAutho.Application.Interfaces;

namespace InnoclinicAutho.Application.UseCases.Common;

public class CurrentUser : IUser
{
    public Guid? Id { get; set; }
    public List<string>? Roles { get; set; }

    public CurrentUser(Guid id, List<string> roles)
    {
        Id = id;
        Roles = roles;
    }
}
