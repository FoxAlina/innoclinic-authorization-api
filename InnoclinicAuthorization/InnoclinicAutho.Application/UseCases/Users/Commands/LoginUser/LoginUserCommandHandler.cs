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
        private readonly IHashService _passwordHasher;
        private readonly IJwtService _jwtService;
        public LoginUserCommandHandler(
            IApiDbContext context,
            IHashService passwordHasher,
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

            if (user == null || !_passwordHasher.VerifyString(request.Password, user.PasswordHash))
            {
                throw new DomainException("Invalid email or password.");
            }

            var token = _jwtService.GenerateToken(user.Id, user.Email, user.Role);

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
