namespace InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.DTOs;
using InnoclinicAutho.Domain.Common;
using InnoclinicAutho.Domain.Entities;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, BaseResponse<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IHashService _passwordHasher;
    private readonly IJwtService _jwtService;
    public LoginUserCommandHandler(
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

    public async Task<BaseResponse<UserDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserDto>();

        try
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user == null || !_passwordHasher.VerifyString(request.Password, user.PasswordHash))
            {
                throw new DomainException("Invalid email or password.");
            }

            var userRole = await _userRoleRepository.GetByUserIdRoleNameAsync(user.Id, UserRoles.Patient.ToString(), cancellationToken);
            if (userRole == null)
            {
                throw new MissingRoleException(user.Email, UserRoles.Patient.ToString());
            }

            var userRoleNames = await _userRoleRepository.GetAllRoleNamesByUserIdAsync(user.Id, cancellationToken);

            var token = _jwtService.GenerateToken(user.Id, user.Email, userRoleNames.ToList());

            response.Data = new UserDto(
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                token);

            if (response.Data is not null)
            {
                response.Succcess = true;
                response.Message = "User logged in successfuly!";
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
