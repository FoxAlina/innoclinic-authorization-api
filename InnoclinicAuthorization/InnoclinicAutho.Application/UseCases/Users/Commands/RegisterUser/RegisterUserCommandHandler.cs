namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.DTOs;
using InnoclinicAutho.Domain.Common;
using InnoclinicAutho.Domain.Entities;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;

public class RegisterUserCommandHandler<T> : IRequestHandler<T, BaseResponse<UserDto>> where T : RegisterUserCommand
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IHashService _passwordHasher;
    private readonly IJwtService _jwtService;

    protected UserRoles _userRole = UserRoles.Patient;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository,
        IHashService passwordHasher,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<BaseResponse<UserDto>> Handle(T request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserDto>();

        try
        {
            var existingUser = _userRepository.GetByEmailAsync(request.Email, cancellationToken).Result;

            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(request.Email);
            }

            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = _passwordHasher.GetHash(request.Password)
            };

            _userRepository.Insert(user);
            await _userRepository.SaveAsync(cancellationToken);

            var role = await _roleRepository.GetByNameAsync(_userRole.ToString(), cancellationToken);
            if (role == null)
            {
                role = new Role
                {
                    RoleName = _userRole.ToString()
                };

                _roleRepository.Insert(role);
                await _roleRepository.SaveAsync(cancellationToken);
            }

            var userRole = await _userRoleRepository.GetByUserIdRoleNameAsync(user.Id, role.RoleName, cancellationToken);
            if (userRole == null)
            {
                userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = role.Id
                };

                _userRoleRepository.Insert(userRole);
                await _userRoleRepository.SaveAsync(cancellationToken);
            }
            
            var token = _jwtService.GenerateToken(user.Id, user.Email, new List<string> { role.RoleName });

            response.Data = new UserDto
                (
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                token);

            if (response.Data is not null)
            {
                response.Succcess = true;
                response.Message = "User registered successfuly!";
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
