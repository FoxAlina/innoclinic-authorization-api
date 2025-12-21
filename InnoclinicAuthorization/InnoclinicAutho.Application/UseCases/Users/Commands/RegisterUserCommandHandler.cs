namespace InnoclinicAutho.Application.UseCases.Users.Commands;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Domain.Common;
using InnoclinicAutho.Domain.Entities;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
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

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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

        var token = _jwtService.GenerateToken(user.ID, user.Email, user.Role);

        return new RegisterUserResponse
            (
            user.ID,
            user.Email,
            user.FirstName,
            user.LastName,
            token);
    }
}
