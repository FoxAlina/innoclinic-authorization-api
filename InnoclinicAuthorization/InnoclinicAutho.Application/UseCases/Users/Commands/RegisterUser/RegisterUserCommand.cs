namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser;

using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.DTOs;
using MediatR;

public record RegisterUserCommand
(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string ConfirmedPassword
) : IRequest<BaseResponse<UserDto>>;
