namespace InnoclinicAutho.Application.UseCases.Users.DTOs;

public record UserDto
(
    Guid UserId,
    string Email,
    string FirstName,
    string LastName,
    List<string> UserRoles,
    string Token
);
