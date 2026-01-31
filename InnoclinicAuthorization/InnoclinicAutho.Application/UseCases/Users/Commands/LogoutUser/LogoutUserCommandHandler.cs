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
    private readonly ICacheService _cacheService;
    public LogoutUserCommandHandler(
        IUserRepository userRepository,
        ICacheService cacheService)
    {
        _userRepository = userRepository;
        _cacheService = cacheService;
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

            var cacheKey = $"user:{user.Id}";
            CachedUser cachedValue = await _cacheService.GetAsync<CachedUser>(cacheKey, cancellationToken);

            if (cachedValue != null)
            {
                await _cacheService.RemoveAsync(cacheKey, cancellationToken);
            }

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
