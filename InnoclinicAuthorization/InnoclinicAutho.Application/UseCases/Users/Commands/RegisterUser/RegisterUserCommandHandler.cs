namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.DTOs;
using InnoclinicAutho.Domain.Common;
using InnoclinicAutho.Domain.Entities;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseResponse<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IHashService _passwordHasher;
    private readonly IJwtService _jwtService;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IHashService passwordHasher,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<BaseResponse<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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
                PasswordHash = _passwordHasher.GetHash(request.Password),
                Role = UserRoles.Patient
            };

            _userRepository.Insert(user);
            await _userRepository.SaveAsync(cancellationToken);

            var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role);

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
