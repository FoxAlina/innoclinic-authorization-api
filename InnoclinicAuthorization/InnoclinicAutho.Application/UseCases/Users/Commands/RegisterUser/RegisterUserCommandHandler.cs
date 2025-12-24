namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser;

using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Domain.Common;
using InnoclinicAutho.Domain.Entities;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseResponse<RegisterUserResponse>>
{
    private readonly IUserRepository _userRepository;
	private readonly IHashService _passwordHasher;
	private readonly IJwtService _jwtService;
	private readonly UserManager<User> _userManager;
	private readonly RoleManager<IdentityRole<Guid>> _roleManager;
	
	public RegisterUserCommandHandler(
		IUserRepository userRepository,
		IHashService passwordHasher,
		IJwtService jwtService,
		UserManager<User> userManager,
		RoleManager<IdentityRole<Guid>> roleManager)
	{
        _userRepository = userRepository;
		_passwordHasher = passwordHasher;
		_jwtService = jwtService;
		_userManager = userManager;
		_roleManager = roleManager;
	}
	
    public async Task<BaseResponse<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<RegisterUserResponse>();

        try
        {
            var existingUser = _userRepository.GetByEmailAsync(request.Email, cancellationToken).Result;

            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(request.Email);
            }

            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var createUserResult = await _userManager.CreateAsync(user, request.Password);

            if (!createUserResult.Succeeded)
                throw new DomainException("User creation failed! Please check user details and try again./nPassword should be non alpanumeric, should have atleast 6 symbols and 1 digit.");

            if (!await _roleManager.RoleExistsAsync(UserRoles.Patient.ToString()))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.Patient.ToString()));

            if (await _roleManager.RoleExistsAsync(UserRoles.Patient.ToString()))
                await _userManager.AddToRoleAsync(user, UserRoles.Patient.ToString());

            var token = _jwtService.GenerateToken(user.Id, user.Email, [UserRoles.Patient.ToString()]);

            response.Data = new RegisterUserResponse (
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                token);

            if (response.Data is not null)
            {
                response.Succcess = true;
                response.Message = "User registered successfuly!";
            }
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
        }

        return response;
    }
}
