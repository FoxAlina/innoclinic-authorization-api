using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.DTOs;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser.CreateUserByAdmin;

public class CreateUserByAdminCommandHandler : RegisterUserCommandHandler<CreateUserByAdminCommand>
{
    public CreateUserByAdminCommandHandler(
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
    { }

    public override async Task<BaseResponse<UserDto>> Handle(CreateUserByAdminCommand request, CancellationToken cancellationToken)
    {
        _userRole = request.UserRole;

        return await base.Handle(request, cancellationToken);
    }
}

