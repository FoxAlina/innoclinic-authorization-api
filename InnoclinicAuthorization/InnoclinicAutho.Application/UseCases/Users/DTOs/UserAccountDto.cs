namespace InnoclinicAutho.Application.UseCases.Users.DTOs;

public record UserAccountDto
(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    List<string> UserRoles
);
