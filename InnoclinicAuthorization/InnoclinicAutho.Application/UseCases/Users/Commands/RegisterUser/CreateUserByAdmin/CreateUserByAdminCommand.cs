using InnoclinicAutho.Application.UseCases.Users.AuthAttributes;
using InnoclinicAutho.Domain.Common;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser.CreateUserByAdmin;

[AuthorizeRoles(Domain.Common.UserRoles.Admin)]
public record CreateUserByAdminCommand(
    UserRoles UserRole,
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string ConfirmedPassword)
    : RegisterUserCommand(
        Email,
        Password,
        FirstName,
        LastName,
        ConfirmedPassword)
{ }
