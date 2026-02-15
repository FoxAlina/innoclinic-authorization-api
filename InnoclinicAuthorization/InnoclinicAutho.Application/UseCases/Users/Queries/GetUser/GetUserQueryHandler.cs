using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.DTOs;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;

namespace InnoclinicAutho.Application.UseCases.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, BaseResponse<UserProfileDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<UserProfileDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserProfileDto>();

        try
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            if (user == null)
            {
                throw new DomainException("User was not found.");
            }

            response.Data = new UserProfileDto(
                user.Email,
                user.FirstName,
                user.LastName);

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
