using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Domain.Common;
using InnoclinicAutho.Domain.Entities;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly IApiDbContext _context;
        private readonly IPasswordHasherNode _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public LoginUserCommandHandler(
            IApiDbContext context,
            IPasswordHasherNode passwordHasher,
            IJwtService jwtService,
            UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Email);

            if (user == null)
                throw new DomainException("Invalid email.");
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                throw new DomainException("Invalid password.");

            var userRoles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user.Id, user.Email, userRoles);

            return new LoginUserResponse(
                user.Id,
                user.Email,
                user.FirstName,
                user.LastName,
                token
            );
        }
    }
}
