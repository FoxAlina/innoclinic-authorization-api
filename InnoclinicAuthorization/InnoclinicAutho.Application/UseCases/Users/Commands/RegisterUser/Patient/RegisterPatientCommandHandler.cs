using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser.Patient;

public class RegisterPatientCommandHandler : RegisterUserCommandHandler<RegisterUserCommand>
{
    public RegisterPatientCommandHandler(
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
        _userRole = Domain.Common.UserRoles.Patient;
    }
}
