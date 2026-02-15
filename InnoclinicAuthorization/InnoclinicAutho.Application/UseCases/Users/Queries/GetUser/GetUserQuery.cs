using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.DTOs;
using MediatR;

namespace InnoclinicAutho.Application.UseCases.Users.Queries.GetUser;

[AuthorizeRoles(Domain.Common.UserRoles.Patient)]
public record GetUserQuery
(
    string Email
) : IRequest<BaseResponse<UserProfileDto>>;
