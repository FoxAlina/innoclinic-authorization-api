using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly IApiDbContext _context;
        private readonly IPasswordHasherNode _passwordHasher;
        private readonly IJwtService _jwtService;
        public LoginUserCommandHandler(
            IApiDbContext context,
            IPasswordHasherNode passwordHasher,
            IJwtService jwtService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(e => e.Email == request.Email);

            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new DomainException("Invalid email or password.");
            }

            var token = _jwtService.GenerateToken(user.ID, user.Email, user.Role);

            return new LoginUserResponse(
                user.ID,
                user.Email,
                user.FirstName,
                user.LastName,
                token
            );
        }
    }
}
