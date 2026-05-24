using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.DTOs;
using InnoclinicAutho.Domain.Common;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;

namespace InnoclinicAutho.Application.UseCases.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, BaseResponse<UserAccountDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;

    public GetUserQueryHandler(
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<BaseResponse<UserAccountDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserAccountDto>();

        try
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user == null)
            {
                throw new DomainException("User was not found.");
            }

            var userRoles = await _userRoleRepository.GetAllRoleNamesByUserIdAsync(request.Id, cancellationToken);

            response.Data = new UserAccountDto(
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                userRoles.ToList());

            if (response.Data is not null)
            {
                response.Succcess = true;
                response.Message = "User was found.";
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
