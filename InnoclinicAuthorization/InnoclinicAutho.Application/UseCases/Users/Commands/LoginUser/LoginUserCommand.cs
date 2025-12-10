using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser
{
    public record LoginUserCommand
    (
        string Email,
        string Password
    ) : IRequest<LoginUserResponse>;
}
