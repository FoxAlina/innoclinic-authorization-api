using InnoclinicAutho.Application.Interfaces;
using InnoclinicAutho.Application.Interfaces.MessageBroker;
using InnoclinicAutho.Application.Interfaces.Repositories;
using InnoclinicAutho.Application.UseCases.Common;
using InnoclinicAutho.Application.UseCases.Users.DTOs;
using InnoclinicRabbitMQContracts.MessageBrokerContracts;
using Microsoft.Extensions.Configuration;

namespace InnoclinicAutho.Application.UseCases.Users.Commands.RegisterUser.Patient;

public class RegisterPatientCommandHandler : RegisterUserCommandHandler<RegisterUserCommand>
{
    private readonly IRabbitMQPublisher<CreatePatientProfile> _rabbitMQPublisher;
    private readonly IConfiguration _configuration;

    public RegisterPatientCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository,
        IHashService passwordHasher,
        IJwtService jwtService,
        IRabbitMQPublisher<CreatePatientProfile> rabbitMQPublisher,
        IConfiguration configuration)
        : base(
            userRepository,
            roleRepository,
            userRoleRepository,
            passwordHasher,
            jwtService)
    {
        _rabbitMQPublisher = rabbitMQPublisher;
        _configuration = configuration;

        _userRole = Domain.Common.UserRoles.Patient;
    }

    public override async Task<BaseResponse<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = await base.Handle(request, cancellationToken);
        
        if (result.Succcess)
        {
            var message = new CreatePatientProfile(request.FirstName, "", request.LastName, request.Email, "", DateTimeOffset.Now);

            await _rabbitMQPublisher.PublishMessageAsync(message);
        }

        return result;
    }
}
