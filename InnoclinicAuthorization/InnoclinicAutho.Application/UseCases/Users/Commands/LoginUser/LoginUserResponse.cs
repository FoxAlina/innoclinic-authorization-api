namespace InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser;

public record LoginUserResponse
(
    Guid UserId,
    string Email,
    string FirstName,
    string LastName,
    string Token
);
