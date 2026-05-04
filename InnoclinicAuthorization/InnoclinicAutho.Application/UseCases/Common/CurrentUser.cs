using InnoclinicAutho.Application.Interfaces;

namespace InnoclinicAutho.Application.UseCases.Common;

public class CurrentUser : IUser
{
    public Guid Id { get; set; }
    public List<string>? Roles { get; set; }

}
