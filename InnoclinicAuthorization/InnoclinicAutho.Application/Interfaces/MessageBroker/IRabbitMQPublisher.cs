namespace InnoclinicAutho.Application.Interfaces.MessageBroker;

public interface IRabbitMQPublisher<T>
{
    public Task PublishMessageAsync(T message);
}
