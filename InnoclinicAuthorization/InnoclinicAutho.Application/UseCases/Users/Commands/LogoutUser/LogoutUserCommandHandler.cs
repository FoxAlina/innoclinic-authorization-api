using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Application.UseCases.Cache;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.LogoutUser;

public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, BaseResponse<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IJwtBlackListService _jwtBlackListService;
    public LogoutUserCommandHandler(
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IJwtBlackListService jwtBlackListService)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _jwtBlackListService = jwtBlackListService;
    }

    public async Task<BaseResponse<string>> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<string>();

        try
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user == null)
            {
                throw new DomainException("User was not found.");
            }
            var cachedUser = new CachedUser(
                    user.Id,
                    user.Email,
                    user.FirstName,
                    user.LastName,
                    await _userRoleRepository.GetAllRoleNamesByUserIdAsync(user.Id, cancellationToken),
                    request.JwtToken);

            await _jwtBlackListService.AddToBlacklistAsync(
                cachedUser,
                request.JwtToken,
                cancellationToken);

            response.Data = "Success";

            if (response.Data is not null)
            {
                response.Succcess = true;
                response.Message = "User logged out successfuly!";
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
