namespace InnoclinicAutho.Application.UseCases.Cache;

public record CachedUser
(
    Guid UserId,
    string Email,
    string FirstName,
    string LastName,
    IEnumerable<string> userRoles,
    string Token
);
