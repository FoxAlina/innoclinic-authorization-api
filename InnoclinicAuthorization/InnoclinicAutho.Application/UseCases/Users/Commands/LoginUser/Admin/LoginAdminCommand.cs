namespace InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser.Admin;

public record LoginAdminCommand(
    string Email,
    string Password) : LoginUserCommand(
        Email,
        Password);
