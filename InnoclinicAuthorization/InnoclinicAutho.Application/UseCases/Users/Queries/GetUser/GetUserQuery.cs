using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.AuthAttributes;
using InnoclinicAutho.Application.UseCases.Users.DTOs;
using MediatR;

namespace InnoclinicAutho.Application.UseCases.Users.Queries.GetUser;

//Todo: Patient - only its profile, Admin - any users profile
[AuthorizeRoles(Domain.Common.UserRoles.Patient)]
public record GetUserQuery
(
    Guid Id
) : IRequest<BaseResponse<UserProfileDto>>;
