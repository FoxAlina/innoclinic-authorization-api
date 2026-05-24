using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.AuthAttributes;
using InnoclinicAutho.Application.UseCases.Users.DTOs;
using MediatR;

namespace InnoclinicAutho.Application.UseCases.Users.Queries.GetUserList;

[AuthorizeRoles(Domain.Common.UserRoles.Admin)]
public record GetUserListQuery : IRequest<BaseResponse<List<UserAccountDto>>>;
