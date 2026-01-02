namespace InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.DTOs;
using InnoclinicAutho.Domain.Common;
using InnoclinicAutho.Domain.Entities;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;

public class LoginUserCommandHandler<T> : IRequestHandler<T, BaseResponse<UserDto>> where T : LoginUserCommand
{
    private readonly IJwtService _jwtService;
	private readonly UserManager<User> _userManager;

    protected UserRoles _userRole = UserRoles.Patient;

    public LoginUserCommandHandler(
		IJwtService jwtService,
		UserManager<User> userManager)
	{
		_jwtService = jwtService;
		_userManager = userManager;
	}

    public async Task<BaseResponse<UserDto>> Handle(T request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserDto>();

        try
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if (user == null)
                throw new DomainException("Invalid email.");
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                throw new DomainException("Invalid password.");

            var isInRole = await _userManager.IsInRoleAsync(user, _userRole.ToString());

            if (!isInRole)
            {
                throw new MissingRoleException(user.Email, _userRole.ToString());
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user.Id, user.Email, userRoles);

            response.Data = new UserDto(
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                token);

            if (response.Data is not null)
            {
                response.Succcess = true;
                response.Message = "User logged in successfuly!";
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
