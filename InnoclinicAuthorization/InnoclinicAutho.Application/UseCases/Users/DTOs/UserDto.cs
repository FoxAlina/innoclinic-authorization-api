namespace InnoclinicAutho.Application.UseCases.Users.DTOs;

public record UserDto
(
    Guid UserId,
    string Email,
    string FirstName,
    string LastName,
    string Token
);
