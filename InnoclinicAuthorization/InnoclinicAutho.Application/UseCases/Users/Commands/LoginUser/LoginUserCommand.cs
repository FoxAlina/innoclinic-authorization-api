namespace InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser;

using MediatR;

public record LoginUserCommand
(
    string Email,
    string Password
) : IRequest<LoginUserResponse>;
