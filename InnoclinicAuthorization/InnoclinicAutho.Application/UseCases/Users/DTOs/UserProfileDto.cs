using InnoclinicAutho.Domain.Common;

namespace InnoclinicAutho.Application.UseCases.Users.DTOs;

public record UserProfileDto
(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    List<string>? UserRoles
);
