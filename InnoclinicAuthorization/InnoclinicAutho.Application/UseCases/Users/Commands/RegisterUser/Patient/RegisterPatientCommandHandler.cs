using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser.Patient;

public class RegisterPatientCommandHandler : RegisterUserCommandHandler<RegisterUserCommand>
{
    public RegisterPatientCommandHandler(
        IUserRepository userRepository, 
        IRoleRepository roleRepository, 
        IUserRoleRepository userRoleRepository, 
        IHashService passwordHasher, 
        IJwtService jwtService)
        : base(
            userRepository, 
            roleRepository, 
            userRoleRepository, 
            passwordHasher, 
            jwtService)
    {
        _userRole = Domain.Common.UserRoles.Patient;
    }
}
