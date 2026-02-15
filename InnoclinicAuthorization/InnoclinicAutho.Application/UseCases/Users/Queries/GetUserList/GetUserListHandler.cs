using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.DTOs;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;

namespace InnoclinicAutho.Application.UseCases.Users.Queries.GetUserList;

public class GetUserListHandler : IRequestHandler<GetUserListQuery, BaseResponse<List<UserProfileDto>>>
{
    private readonly IUserRepository _userRepository;
    public GetUserListHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<List<UserProfileDto>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<List<UserProfileDto>>();

        try
        {
            var userList = await _userRepository.GetAllAsync();

            if (userList == null)
            {
                throw new DomainException("Users info was not found.");
            }

            List<UserProfileDto> userProfileDtos = new List<UserProfileDto>();

            foreach (var user in userList) {
                userProfileDtos.Add(new UserProfileDto (
                    user.Email,
                    user.FirstName,
                    user.LastName));
            }

            response.Data = userProfileDtos;

            if (response.Data is not null)
            {
                response.Succcess = true;
                response.Message = "Users info found.";
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
