using InnoclinicAutho.Application.Interfaces.MessageBroker;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace InnoclinicAutho.Infrastructure.MessageBroker;

public class RabbitMQPublisher<T> : IRabbitMQPublisher<T>
{
    private readonly IPublishEndpoint _publisher;
    //private readonly ISendEndpointProvider _publisher;
    private readonly IConfiguration _configuration;
    public RabbitMQPublisher(IPublishEndpoint publisher, IConfiguration configuration)
    {
        _publisher = publisher;
        _configuration = configuration;
    }

    public async Task PublishMessageAsync(T message)
    {
        await _publisher.Publish(message);

        //if (message is not null)
        //{
        //    var endpoint = await _publisher.GetSendEndpoint(new Uri(_configuration["RabbitMQSettings:UserProfileMaintainQueue"]));

        //    await endpoint.Send(message);
        //}
    }
}
