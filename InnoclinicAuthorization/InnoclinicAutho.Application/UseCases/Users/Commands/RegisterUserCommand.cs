using MediatR;

namespace InnoclinicAutho.Application.UseCases.Users.Commands
{
    public record RegisterUserCommand
    (
        string Email,
        string Password,
        string FirstName,
        string LastName,
        string ConfirmedPassword
    ) : IRequest<RegisterUserResponse>;

    public record RegisterUserResponse
    (
        Guid UserId,
        string email,
        string FirstName,
        string LastName,
        string Token
    );
}
