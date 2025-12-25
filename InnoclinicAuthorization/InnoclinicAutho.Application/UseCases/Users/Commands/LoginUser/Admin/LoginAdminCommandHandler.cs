using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser.Admin;

public class LoginAdminCommandHandler : LoginUserCommandHandler<LoginAdminCommand>
{
    public LoginAdminCommandHandler(IJwtService jwtService, UserManager<User> userManager) : base(jwtService, userManager)
    {
        _userRole = Domain.Common.UserRoles.Admin;
    }
}
