using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Domain.Common;
using InnoclinicAutho.Domain.Entities;
using InnoclinicAutho.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InnoclinicAutho.Application.UseCases.Users.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IApiDbContext _context;
        private readonly IPasswordHasherNode _passwordHasher;
        private readonly IJwtService _jwtService;

        public RegisterUserCommandHandler(
            IApiDbContext context,
            IPasswordHasherNode passwordHasher,
            IJwtService jwtService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(request.Email);
            }

            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                Role = UserRoles.Patient
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            var token = _jwtService.GenerateToken(user.ID, user.Email, user.Role);

            return new RegisterUserResponse
                (
                user.ID,
                user.Email,
                user.FirstName,
                user.LastName,
                token
                );
        }
    }
}
