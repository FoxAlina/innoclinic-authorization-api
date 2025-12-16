using System;
using System.Collections.Generic;
using System.Text;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser
{
    public record RegisterUserResponse
    (
        Guid UserId,
        string Email,
        string FirstName,
        string LastName,
        string Token
    );
}
