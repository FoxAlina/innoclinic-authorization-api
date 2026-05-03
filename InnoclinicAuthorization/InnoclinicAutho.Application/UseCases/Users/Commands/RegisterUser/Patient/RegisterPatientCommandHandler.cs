using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser.Patient;

public class RegisterPatientCommandHandler : RegisterUserCommandHandler<RegisterUserCommand>
{
    public RegisterPatientCommandHandler(
        IUserRepository userRepository, 
        IRoleRepository roleRepository,
        IHashService passwordHasher, 
        IJwtService jwtService)
        : base(
            userRepository, 
            roleRepository,
            passwordHasher, 
            jwtService)
    {
        _userRole = Domain.Common.UserRoles.Patient;
    }
}
