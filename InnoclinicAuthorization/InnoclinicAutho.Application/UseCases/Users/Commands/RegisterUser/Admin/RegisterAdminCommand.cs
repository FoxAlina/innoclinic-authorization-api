namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser.Admin;

public record RegisterAdminCommand(
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
        ConfirmedPassword) { }