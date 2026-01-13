namespace InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser;

using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.DTOs;
using MediatR;

public record LoginUserCommand
(
    string Email,
    string Password
) : IRequest<BaseResponse<UserDto>>;
