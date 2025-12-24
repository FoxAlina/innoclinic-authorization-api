namespace InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Domain.Entities;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, BaseResponse<LoginUserResponse>>
{
	private readonly IHashService _passwordHasher;
	private readonly IJwtService _jwtService;
	private readonly UserManager<User> _userManager;
	private readonly RoleManager<IdentityRole<Guid>> _roleManager;
	
	public LoginUserCommandHandler(
        IHashService passwordHasher,
		IJwtService jwtService,
		UserManager<User> userManager,
		RoleManager<IdentityRole<Guid>> roleManager)
	{
		_passwordHasher = passwordHasher;
		_jwtService = jwtService;
		_userManager = userManager;
		_roleManager = roleManager;
	}

    public async Task<BaseResponse<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<LoginUserResponse>();

        try
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if (user == null)
                throw new DomainException("Invalid email.");
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                throw new DomainException("Invalid password.");

            var userRoles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user.Id, user.Email, userRoles);

            response.Data = new LoginUserResponse(
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
