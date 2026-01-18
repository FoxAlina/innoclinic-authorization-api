using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser.Admin;

public class RegisterAdminCommandHandler : RegisterUserCommandHandler<RegisterAdminCommand>
{
    public RegisterAdminCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository,
        IHashService passwordHasher,
        IJwtService jwtService,
        ICacheService cacheService)
        : base(
            userRepository, 
            roleRepository, 
            userRoleRepository, 
            passwordHasher, 
            jwtService, 
            cacheService)
    {
        _userRole = Domain.Common.UserRoles.Admin;
    }
}
