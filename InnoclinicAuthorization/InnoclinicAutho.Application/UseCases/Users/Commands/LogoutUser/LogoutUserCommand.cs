using InnoclinicAutho.Application.UseCases.Common;
using MediatR;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.LogoutUser;

public record LogoutUserCommand
(
    string Email
) : IRequest<BaseResponse<string>>;
