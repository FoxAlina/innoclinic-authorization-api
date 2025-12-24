namespace InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, BaseResponse<LoginUserResponse>>
{
    private readonly IApiDbContext _context;
    private readonly IHashService _passwordHasher;
    private readonly IJwtService _jwtService;
    public LoginUserCommandHandler(
        IApiDbContext context,
        IHashService passwordHasher,
        IJwtService jwtService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<BaseResponse<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<LoginUserResponse>();

        try
        {
            var user = await _context.Users
            .FirstOrDefaultAsync(e => e.Email == request.Email);

            if (user == null || !_passwordHasher.VerifyString(request.Password, user.PasswordHash))
            {
                throw new DomainException("Invalid email or password.");
            }

            var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role);

            response.Data = new LoginUserResponse(
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
