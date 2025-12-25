using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser.Admin;

public class RegisterAdminCommandHandler : RegisterUserCommandHandler<RegisterAdminCommand>
{
    public RegisterAdminCommandHandler(
        IUserRepository userRepository, 
        IHashService passwordHasher, 
        IJwtService jwtService, 
        UserManager<User> userManager, 
        RoleManager<IdentityRole<Guid>> roleManager) 
        : base(
            userRepository, 
            passwordHasher, 
            jwtService, 
            userManager, 
            roleManager) 
    {
        _userRole = Domain.Common.UserRoles.Admin;
    }

}
