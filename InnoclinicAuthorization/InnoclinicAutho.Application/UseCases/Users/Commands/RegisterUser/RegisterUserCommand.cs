using MediatR;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser
{
    public record RegisterUserCommand
    (
        string Email,
        string Password,
        string FirstName,
        string LastName,
        string ConfirmedPassword
    ) : IRequest<RegisterUserResponse>;

}
