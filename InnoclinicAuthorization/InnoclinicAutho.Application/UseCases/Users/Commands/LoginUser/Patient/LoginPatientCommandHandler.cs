using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser.Patient;

public class LoginPatientCommandHandler : LoginUserCommandHandler<LoginUserCommand>
{
    public LoginPatientCommandHandler(IJwtService jwtService, UserManager<User> userManager) : base(jwtService, userManager)
    {
        _userRole = Domain.Common.UserRoles.Patient;
    }
}
