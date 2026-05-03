namespace InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.DTOs;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, BaseResponse<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IHashService _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly ICacheService _cacheService;
    public LoginUserCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository,
        IHashService passwordHasher,
        IJwtService jwtService,
        ICacheService cacheService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _cacheService = cacheService;
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

            var userRoleNames = await _userRoleRepository.GetAllRoleNamesByUserIdAsync(user.Id, cancellationToken);

            string token = _jwtService.GenerateToken(user.Id, user.Email, userRoleNames.ToList());

            response.Data = new UserDto(
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                userRoleNames.ToList(),
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
